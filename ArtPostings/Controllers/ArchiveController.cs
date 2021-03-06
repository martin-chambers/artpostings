﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IPostingService service;
        public ArchiveController(IPostingService _service)
        {
            service = _service;
        }

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
            ViewBag.Header = service.ArchiveHeader;
            ViewBag.Mode = "_itemDisplay";
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ExtendedJsonResult Edit([Bind(Include = "Editing, ItemPosting")]ItemPostingViewModel posting)
        {
            setItemInfo();
            ChangeResult result = service.SaveArchiveChanges(posting);
            return new ExtendedJsonResult(result) { StatusCode = result.StatusCode };
        }
    }
}