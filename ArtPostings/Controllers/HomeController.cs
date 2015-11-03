using System;
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
        private readonly IPostingService service;
        public HomeController(IPostingService _service)
        {
            service = _service;
        }
        /// <summary>
        /// default parameterless constructor required by asp.net MVC framework
        /// hard-coded here to default to the concrete PostingService class.
        /// This will be replaced by IoC in future
        /// </summary>
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Editing, ItemPosting")]ItemPostingViewModel posting)
        {
            setItemInfo();
            ChangeResult result = service.SaveShopChanges(posting);
            return new ExtendedJsonResult(result) { StatusCode = result.StatusCode };
        }
        private void setItemInfo()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_itemDisplay";
        }

    }
}
