using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;


namespace AnnChambersArt.Controllers
{
    public class AboutController : Controller
    {
        private IPostingRepository repository;
        public AboutController(IPostingRepository _repository)
        {
            repository = _repository;
        }
        public AboutController() : this(new PostingRepository()) { }

        // GET: About
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View(repository.ShopPostings());
        }
    }
}