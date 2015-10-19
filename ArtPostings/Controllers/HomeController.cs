//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;
//using System.Net;

namespace ArtPostings.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IPostingService service;
        public HomeController(IPostingService _service)
        {
            service = _service;
        }
        public HomeController() : this(PostingService.Instance) { }

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
        private void setItemInfo()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_itemDisplay";
        }

    }
}
