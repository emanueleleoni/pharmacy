﻿using Marcucci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marcucci.Controllers
{
    public class ThankYouController : Controller
    {
        // GET: ThankYou
        public ActionResult Index()
        {
            // Svuota il carrello
            ViewBag.cartBox     = null;
            ViewBag.Total       = null;
            ViewBag.NoOfItem    = null;
            TempShpData.items   = null;

            return View("Thankyou");
        }
    }
}