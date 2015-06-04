using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;

namespace AnnChambersArt.Controllers
{
    public class ContactController : Controller
    {
        private IPostingService service;
        public ContactController(IPostingService _service)
        {
            service = _service;
        }
        public ContactController() : this(PostingService.Instance) { }

        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View(service.ShopPostings());
        }
    }
}