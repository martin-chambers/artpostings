using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;

namespace AnnChambersArt.Controllers
{
    public class LinksController : Controller
    {
        private IPostingRepository repository;
        public LinksController(IPostingRepository _repository)
        {
            repository = _repository;
        }
        public LinksController() : this(new PostingRepository()) { }

        // GET: Links
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View(repository.ShopPostings());
        }
    }
}