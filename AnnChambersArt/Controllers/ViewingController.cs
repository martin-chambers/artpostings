using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;

namespace AnnChambersArt.Controllers
{
    public class ViewingController : Controller
    {
        private IPostingRepository repository;
        public ViewingController(IPostingRepository _repository)
        {
            repository = _repository;
        }
        public ViewingController() : this(new PostingRepository()) { }

        // GET: Viewing
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View(repository.ShopPostings());
        }
    }
}