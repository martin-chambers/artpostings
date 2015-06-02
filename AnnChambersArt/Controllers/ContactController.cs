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
        private IPostingRepository repository;
        public ContactController(IPostingRepository _repository)
        {
            repository = _repository;
        }
        public ContactController() : this(new PostingRepository()) { }

        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View(repository.ShopPostings());
        }
    }
}