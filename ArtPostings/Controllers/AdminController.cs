using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index()
        {
            ViewBag.Mode = "_noItemDisplay";
            List<PictureFileRecord> fileRecords = new List<PictureFileRecord>();
            fileRecords = service.PictureFileRecordList();
            return View(fileRecords);
        }       

    }
}