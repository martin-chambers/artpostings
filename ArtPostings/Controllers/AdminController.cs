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
                return PartialView("_PictureList", load(status));
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
            // currently have a problem with sorting after deleting !!!!!!!
            try
            {
                if (rec.FileName == null)
                {
                    throw new ArgumentNullException("The view has fired the delete file process unexpectedly");
                }
                else
                {
                    List<PictureFileRecord> fileRecords =
                        service.DeletePictureFile(rec.FileName, service.FullyMappedPictureFolder).ToList();
                }
            }
            catch (ArgumentNullException anEx)
            {
                // log but don't halt execution - the javascript function has fired, most likely because 
                // of difficult-to-understand interaction between the paging control and onclick event in webgrid
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(anEx));
            }
            return RedirectToAction("Index", new { status = PictureFileRecord.GetStatusString(rec.Status), initial = false });
        }
        

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
            return RedirectToAction("Index", new { status = "All", initial = false });
        }

        [HttpPost]
        public ActionResult MovePicture(string filepath, bool archive)
        {
            string filename = Utility.GetFilenameFromFilepath(filepath);
            IEnumerable<ItemPostingViewModel> postingVMs = (archive) ? service.ArchivePostings() : service.ShopPostings();
            foreach (ItemPostingViewModel vm in postingVMs)
            {
                if (filename == vm.ItemPosting.FileName)
                {
                    //return Json(new { success = false, responsetext = vm.ItemPosting.FileName + " is already included on the archive page" });
                    return new ExtendedJsonResult(
                            new
                            {
                                success = false,
                                message = vm.ItemPosting.FileName + " is already included in the " + ((archive) ? "Archive" : "Home") + " page"
                            }
                    )
                    { StatusCode = 409 };
                }
            }
            PictureFileRecord pfr = new PictureFileRecord(filepath);
            ChangeResult result = (archive) ? service.InsertArchivePosting(pfr) : service.InsertShopPosting(pfr);
            
            return new ExtendedJsonResult(result) { StatusCode = result.StatusCode };
        }

    }
}