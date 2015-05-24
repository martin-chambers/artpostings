using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnnChambersArt.Models;

namespace AnnChambersArt.Controllers
{
    public class ArchiveController : Controller
    {
        private IPostingRepository repository;
        public ArchiveController(IPostingRepository _repository)
        {
            repository = _repository;
        }
        public ArchiveController() : this(new PostingRepository()) { }


        // GET: Archive
        public ActionResult Index()
        {
            ViewBag.Mode = "_archive";        
            return View(repository.ListPostings());
        }
    }
}