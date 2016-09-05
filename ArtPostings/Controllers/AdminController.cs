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
        // GET: Admin
        public ActionResult Index(string status = "All", bool initial = true)
        {
            ViewBag.Admin = true;
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

        private PictureFileRecordsViewModel load(string status, object data = null)
        {
            List<PictureFileRecord> fileRecords =
                service.PictureFileRecordList(service.FullyMappedPictureFolder, status).ToList();
            PictureFileRecordsViewModel viewModel = new PictureFileRecordsViewModel(fileRecords, PictureFileRecord.StatusList, status);
            // need to return partialview here or _PictureList repeats the _Layout components on the webpage
            return viewModel;
        }

        [HttpPost]
        public ActionResult FileDelete(PictureFileRecord rec)
        {
            ChangeResult result = new ChangeResult();
            string status = PictureFileRecord.GetStatusString(rec.Status);
            bool display =
                rec.Status == PictureFileRecord.StatusType.Archived || rec.Status == PictureFileRecord.StatusType.ForSale;
            bool archive = rec.Status == PictureFileRecord.StatusType.Archived;
                        try
            {
                //List<PictureFileRecord> fileRecords =
                //    service.DeletePictureFile(rec.FileName, archive, display, service.FullyMappedPictureFolder).ToList();
                result = service.DeletePictureFile(rec.FileName, archive, display, service.FullyMappedPictureFolder);
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
            //var jsondata = new
            //{
            //    data = result,
            //    view = RenderHelper.PartialView(this, (display) ? "_PictureList" : "_NonOrderablePictureList", load(status))
            //};
            

        }

        /// <summary>
        /// Promotes the item in its list
        /// This action reduces the order number of the item by one if the order value is greater than zero. 
        /// The order value of the preceding item is increased by one.
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
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
            return RedirectToAction("Index", new { status = "NotDisplayed", initial = false });
        }

        [HttpPost]
        public ActionResult MovePicture(string filepath, bool archivedestination, bool displaydestination)
        {
            ChangeResult result = service.MovePicture(filepath, archivedestination, displaydestination);
            return new ExtendedJsonResult(result);
        }
    }
}