﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditPand.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       

        public ActionResult Register()
        {
            ViewBag.Message = "One Page transform.";

            return View();
        }

        public ActionResult ClientCards()
        {
            ViewBag.Message = "One Page transform.";

            return View();
        }
        public ActionResult AdminCards()
        {
            ViewBag.Message = "Administracion de Tarjetas.";

            return View();
        }
        public ActionResult AdminCharts()
        {
            ViewBag.Message = "Administracion de Reportes.";

            return View();
        }
        public ActionResult User()
        {
            ViewBag.Message = "Administracion de usuarios.";

            return View();
        }
    }
}