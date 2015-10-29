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

        private IPostingService service;
        public AdminController(IPostingService _service)
        {
            service = _service;
        }
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

        //public ActionResult Filter (string status)
        //{
        //    return PartialView("_PictureList", load(status));
        //}

        private PictureFileRecordsViewModel load(string status)
        {
            List<PictureFileRecord> fileRecords =
                service.PictureFileRecordList(status).ToList();
            PictureFileRecordsViewModel viewModel = new PictureFileRecordsViewModel(fileRecords, PictureFileRecord.StatusList, status);
            // need to return partialview here or _PictureList repeats the _Layout components on the webpage
            return viewModel;
        }
    }
}