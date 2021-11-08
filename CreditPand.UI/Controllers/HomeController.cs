using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditPand.UI.Controllers
{
    public class HomeController : Controller
    {
        //Action del Index, inicio de la página de CreditPand
        public ActionResult Index()
        {
            return View();
        }

        //Muestra la información relacionada a la empresa
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Muestra el formulario y demás datos de contacto
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Muestra las políticas de privacidad de la empresa
        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "Your policy page.";

            return View();
        }

        //Muestra la parte de soporte de la empresa 
        public ActionResult Support()
        {
            ViewBag.Message = "Your support page.";

            return View();
        }

        public ActionResult AdminCharts() //En otro Controlador
        {
            ViewBag.Message = "Administracion de Reportes.";

            return View();
        }

        /*public ActionResult User() //En otro Controlador, 
        {
            ViewBag.Message = "Administracion de usuarios.";

            return View();
        }*/
    }
}