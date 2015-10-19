using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
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
            setItemInfo();
            return View(service.ArchivePostings());
        }
        public ActionResult Reload()
        {
            setItemInfo();
            return PartialView("_itemDisplay", service.ArchivePostings());
        }
        public ActionResult Edit(int id)
        {
            setItemInfo();
            return PartialView("_itemDisplay", service.EditModeArchivePostings(id));
        }
        private void setItemInfo()
        {
            ViewBag.Blurb = service.ArchiveText;
            ViewBag.Mode = "_itemDisplay";
        }

    }
}