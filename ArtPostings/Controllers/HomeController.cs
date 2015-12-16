﻿using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostingService service;
        public HomeController(IPostingService _service)
        {
            service = _service;
        }

        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {            
            setItemInfo();
            return View(service.ShopPostings());
        }
        public ActionResult Reload()
        {
            setItemInfo();
            return PartialView("_itemDisplay", service.ShopPostings());
        }
        public ActionResult Edit(int id)
        {
            setItemInfo();
            return PartialView("_itemDisplay", service.EditModeShopPostings(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Editing, ItemPosting")]ItemPostingViewModel posting)
        {
            setItemInfo();
            ChangeResult result = service.SaveShopChanges(posting);
            return new ExtendedJsonResult(result);
        }
        private void setItemInfo()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_itemDisplay";
        }

    }
}
