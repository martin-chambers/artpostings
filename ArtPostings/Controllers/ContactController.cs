using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    public class ContactController : Controller
    {
        private readonly IPostingService service;
        public ContactController(IPostingService _service)
        {
            service = _service;
        }

        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Mode = "_noItemDisplay";
            return View();
        }
    }
}