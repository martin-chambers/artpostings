﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtPostings.Models;


namespace ArtPostings.Controllers
{
    public class AboutController : Controller
    {
        private readonly IPostingService service;
        public AboutController(IPostingService _service)
        {
            service = _service;
        }
        
        // GET: About
        public ActionResult Index()
        {
            ViewBag.Mode = "_noItemDisplay";
            return View();
        }
    }
}