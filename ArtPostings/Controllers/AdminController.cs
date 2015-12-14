using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ArtPostings.Models;


namespace ArtPostings.Controllers
{
    public class AdminController : Controller
    {

        private readonly IPostingService service;
        private string webSafePictureFolder = ConfigurationManager.AppSettings["pictureLocation"];

        public AdminController(IPostingService _service)
        {
            service = _service;
        }
        /// <summary>
        /// default parameterless constructor required by asp.net MVC framework
        /// hard-coded here to default to the concrete PostingService class.
        /// This will be replaced by IoC in future
        /// </summary>
        public AdminController() : this(PostingService.Instance) { }
        // GET: Admin
        public ActionResult Index(string status = "All", bool initial = true)
        {
            ViewBag.Mode = "_noItemDisplay";
            if (initial || !Request.IsAjaxRequest())
            {
                return View(load(status));
            }
            else
            {
                if (status == "All" || status == "NotDisplayed")
                {
                    return PartialView("_NonOrderablePictureList", load(status));
                }
                else
                {
                    return PartialView("_PictureList", load(status));
                }
            }
        }

        private PictureFileRecordsViewModel load(string status)
        {
            List<PictureFileRecord> fileRecords =
                service.PictureFileRecordList(service.FullyMappedPictureFolder, status).ToList();
            PictureFileRecordsViewModel viewModel = new PictureFileRecordsViewModel(fileRecords, PictureFileRecord.StatusList, status);
            // need to return partialview here or _PictureList repeats the _Layout components on the webpage
            return viewModel;
        }

        public ActionResult FileDelete(PictureFileRecord rec)
        {
            try
            {
                bool display =
                    rec.Status == PictureFileRecord.StatusType.Archived || rec.Status == PictureFileRecord.StatusType.ForSale;
                bool archive = rec.Status == PictureFileRecord.StatusType.Archived;
                List<PictureFileRecord> fileRecords =
                    service.DeletePictureFile(rec.FileName, archive, display, service.FullyMappedPictureFolder).ToList();
            }
            catch (Exception anEx)
            {
                // log but don't halt execution - the javascript function has fired, possibly at an inappropriate time. 
                // This is most likely because of not-fully-understood interaction between the paging control and onclick 
                // event in webgrid
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(anEx));
            }
            // RedirectToAction works by sending an http 302 response to browser which causes the 
            // browser to make a GET request to the action - this is precisely what we want
            return RedirectToAction("Index", new { status = PictureFileRecord.GetStatusString(rec.Status), initial = false });
        }

        /// <summary>
        /// Promotes the item in its list
        /// This action reduces the order number of the item by one if the order value is greater than zero. 
        /// The order value of the preceding item is increased by one.
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FilePromote(PictureFileRecord rec)
        {
            ChangeResult result = new ChangeResult();
            try
            {
                result = service.AdvanceInList(rec);
            }
            catch (Exception anEx)
            {
                // log but don't halt execution
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(anEx));
            }
            // RedirectToAction works by sending an http 302 response to browser which causes the 
            // browser to make a GET request to the action - this is precisely what we want
            return RedirectToAction("Index", new { status = PictureFileRecord.GetStatusString(rec.Status), initial = false });
        }

        /// <summary>
        /// Called from within a view form. Uploads a selected set of files (which is identified 
        /// as part of the current request) to the configured picture folder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            //this code could handle multiple files as part of an upload
            //the initial implementation of file uploading in the application will not require this
            foreach (string upload in Request.Files)
            {
                if (!Request.Files[upload].HasFile()) continue;
                string path = service.FullyMappedPictureFolder;
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(path, filename));
            }
            // RedirectToAction works by sending an http 302 response to browser which causes the 
            // browser to make a GET request to the action - this is precisely what we want
            return RedirectToAction("Index", new { status = "All", initial = false });
        }

        [HttpPost]
        public ActionResult MovePicture(string filepath, bool archivedestination, bool displaydestination)
        {
            // filepaths / names brought in from ajax call will have %20 spaces and will not have JS escaped single quotes
            string filename = Utility.GetFilenameFromFilepath(filepath).Normalise();

            // default constructor gives failed results
            ChangeResult removeResult = new ChangeResult();
            ChangeResult insertResult = new ChangeResult();
            // initialising variables
            PictureFileRecord pfr = new PictureFileRecord(filepath);
            PictureFileRecord.StatusType source;
            List<ItemPostingViewModel> postingVMs = new List<ItemPostingViewModel>();

            // getting current database contents
            postingVMs.AddRange(service.ArchivePostings().ToList());
            postingVMs.AddRange(service.ShopPostings().ToList());
            // getting full info on item to move 
            ItemPostingViewModel moveItem = postingVMs.FirstOrDefault(x => x.ItemPosting.FileName.ToUpper().Normalise() == filename.ToUpper());
            ItemPosting posting = new ItemPosting();
            // determine current location of move item
            if (moveItem == null)
            {
                source = PictureFileRecord.StatusType.NotDisplayed;
            }
            else
            {
                posting = moveItem.ItemPosting;
                if (posting.Archive_Flag == true)
                {
                    source = PictureFileRecord.StatusType.Archived;
                }
                else
                {
                    source = PictureFileRecord.StatusType.ForSale;
                }
            }
            if (displaydestination)
            {
                // (1) the move item's being moved to the list it's already in
                if (archivedestination && source == PictureFileRecord.StatusType.Archived ||
                    !archivedestination && source == PictureFileRecord.StatusType.ForSale)
                {
                    return new ExtendedJsonResult(
                        new ChangeResult(
                            false,
                            filename + " is already included in the " + ((archivedestination) ? "Archive" : "Home") + " page"),
                            400);
                }
                else
                {
                    // (2) the moveItem's being moved from one list to another
                    if (archivedestination && source == PictureFileRecord.StatusType.ForSale ||
                        !archivedestination && source == PictureFileRecord.StatusType.Archived)
                    {
                        removeResult = service.RemoveFromDisplay(posting);
                        insertResult = service.InsertPosting(pfr, archivedestination);
                        return new ExtendedJsonResult(insertResult, insertResult.StatusCode);
                    }
                    else
                    {
                        // (3) the moveItem's being moved onto a list from not_displayed
                        insertResult = service.InsertPosting(pfr, archivedestination);
                        return new ExtendedJsonResult(insertResult, insertResult.StatusCode);
                    }
                }
            }
            // (4) the moveItem's being removed from all lists
            else
            {
                if (source == PictureFileRecord.StatusType.NotDisplayed)
                {
                    return new ExtendedJsonResult(
                        new ChangeResult(
                            false,
                            filename + " is not currently displayed, and therefore is not in the database. Therefore it cannot be removed from the database"),
                        500);
                }
                else
                {
                    removeResult = service.RemoveFromDisplay(posting);
                    return new ExtendedJsonResult(removeResult, removeResult.StatusCode);
                }
            }
        }
    }
}