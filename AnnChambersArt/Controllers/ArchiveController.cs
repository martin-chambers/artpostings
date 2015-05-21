using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnChambersArt.Controllers
{
    public class ArchiveController : Controller
    {
        // GET: Archive
        public ActionResult Index()
        {
            ViewBag.Mode = "_archive";        
            return View();
        }


        // I want to use the techniques outlined here to decouple controllers, logic and data access
        // http://www.asp.net/mvc/overview/older-versions-1/models-(data)/validating-with-a-service-layer-cs


    }
}