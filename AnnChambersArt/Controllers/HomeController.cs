using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;

namespace AnnChambersArt.Controllers
{
    public class HomeController : Controller
    {
        private IPostingRepository repository;
        public HomeController(IPostingRepository _repository)
        {
            repository = _repository;
        }
        public HomeController() : this(new PostingRepository()) { }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View(repository.ShopPostings());
        }
    }
}
