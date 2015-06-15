using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;


namespace ArtPostings.Controllers
{
    public class AboutController : Controller
    {
 private IPostingService service;
        public AboutController(IPostingService _service)
        {
            service = _service;
        }
        public AboutController() : this(PostingService.Instance) { }

        // GET: About
        public ActionResult Index()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_noItemDisplay";
            return View(service.ShopPostings());
        }
    }
}