using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    public class ViewingController : Controller
    {
        private IPostingService service;
        public ViewingController(IPostingService _service)
        {
            service = _service;
        }
        public ViewingController() : this(PostingService.Instance) { }
        
        // GET: Viewing
        public ActionResult Index()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_shopwindow";
            return View(service.ShopPostings());
        }
    }
}