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
        private readonly IPostingService service;
        public ViewingController(IPostingService _service)
        {
            service = _service;
        }
        
        // GET: Viewing
        public ActionResult Index()
        {
            throw new HttpException(404, "Not found"); // this view has been removed
            /*
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_noItemDisplay";
            return View();
            */
        }
    }
}