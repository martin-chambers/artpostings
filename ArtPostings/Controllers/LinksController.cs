﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;

namespace ArtPostings.Controllers
{
    public class LinksController : Controller
    {
        private readonly IPostingService service;
        public LinksController(IPostingService _service)
        {
            service = _service;
        }
        
        // GET: Links
        public ActionResult Index()
        {
            ViewBag.Blurb = service.ShopText;
            ViewBag.Mode = "_noItemDisplay";
            return View();
        }
    }
}