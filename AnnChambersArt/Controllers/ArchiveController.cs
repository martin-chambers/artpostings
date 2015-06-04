using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;

namespace AnnChambersArt.Controllers
{
    public class ArchiveController : Controller
    {
        private IPostingService service;
        public ArchiveController(IPostingService _service)
        {
            service = _service;
        }
        public ArchiveController() : this(PostingService.Instance) { }


        // GET: Archive
        public ActionResult Index()
        {
            ViewBag.Mode = "_archive";
            return View(service.ArchivePostings());
        }
    }
}