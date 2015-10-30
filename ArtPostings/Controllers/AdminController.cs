using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    public class AdminController : Controller
    {

        private readonly IPostingService service;
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
            return PartialView("_PictureList", load(PictureFileRecord.GetStatusString(rec.Status)));
        }
    }
}