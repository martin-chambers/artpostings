using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnChambersArt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Mode = "_shopwindow";
            return View();
        }
        public ActionResult Archive()
        {
            ViewBag.Mode = "_archive";
            return View();
        }
        public ActionResult Links()
        {
            ViewBag.Mode = "_shopwindow";
            return View();
        }

        public ActionResult ViewLocation()
        {
            ViewBag.Mode = "_shopwindow";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Mode = "_shopwindow";
            ViewBag.Message = "";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Mode = "_shopwindow";
            return View();
        }
    }
}
