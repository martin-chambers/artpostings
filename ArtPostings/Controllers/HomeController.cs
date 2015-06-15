using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    public class HomeController : Controller
    {
        private IPostingService service;
        public HomeController(IPostingService _service)
        {
            service = _service;
        }
        public HomeController() : this(PostingService.Instance) { }
        
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_itemDisplay";
            return View(service.ShopPostings());
        }
    }
}
