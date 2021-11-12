using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Windows;
using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using CreditPand.BD.Repositorios;

namespace CreditPand.UI.Controllers
{
    public class TarjetaController : Controller
    {
        private readonly IGestorTarjeta _oGestorTarjeta;

        //Constructor de la tarjeta
        public TarjetaController()
        {
            _oGestorTarjeta = new GestorTarjeta();
        }


        // GET: Tarjeta
        //Muestra el formulario para el ajuste de los intereses morosos y regulares
        public ActionResult InteresesForm()
        {
            return View();
        }


        //Permite guardar los interes ajustados en la base de datos
        public ActionResult InteresesConfigure(Interes objInteres)
        {
            int intereses = _oGestorTarjeta.ActualizarInteres(objInteres);
            //Swal.fire("Registro Exitoso"); //Alert de registro exitoso del interes
            MessageBox.Show("Intereses cambiados");//Cambiar por el sweetalert
            return RedirectToAction("InteresesForm");
        }


        //Formulario para la solicitud de una tarjeta de crédito por parte de un cliente
        public ActionResult ClientCards()
        {
            return View();
        }


        //Para enviar la solicitud de una tarjeta de crédito por parte de un cliente
        public ActionResult EnviarSolicitud(Tarjeta pTarjeta)
        {
            //FALTA CÓDIGO PARA PASAR LOS DATOS DE UNA VISTA A OTRA
            return View();
        }


        //Vista que muestra las solicitudes de tarjetas 
        public ActionResult SolicitudesTarjetas()
        {
            return View();
        }



        //Para registrar una tarjeta de credito
        public ActionResult RegistroTarjeta(Tarjeta pTarjeta)
        {
            int registros = _oGestorTarjeta.CrearTarjeta(pTarjeta);
            //Swal.fire("Registro Exitoso"); //Alert de registro exitoso
            return RedirectToAction("ClientCards");
        }

        //Muestra todas las tarjetas registradas a las que se les puede dar mantenimiento
        public ActionResult ConsultCards(DateTime? Fecha_activación= null, string Marca =  null, int? Límite = null)
        {
           
            /*var tarjetas = _services.ObtenerPaginaDePersonasFiltrada(page, PERSONAS_POR_PAGINA,
                                   sort, sortDir, buscar, minHijos, maxHijos);*/


            IEnumerable<Tarjeta> Cards = _oGestorTarjeta.ListadoTarjetas();
            return View(Cards);

        }

        //Método que permite exportar la información de las tarjetas a Excel
        public void DescargarExcel()
        {
            List<Tarjeta> exceldata = new List<Tarjeta>();
            using (CreditPandEntities ContextoDB = new CreditPandEntities())
            {
                exceldata = ContextoDB.Tarjeta.ToList();
            }
            //create object to webgrid  
            WebGrid webGrid = new WebGrid(source: exceldata, canPage: false, canSort: false);
            string gridData = webGrid.GetHtml(
            columns: webGrid.Columns(
                            webGrid.Column(columnName: "Id", header: "ID"),
                            webGrid.Column(columnName: "Marca", header: "Marca"),
                            webGrid.Column(columnName: "Límite", header: "Límite"),
                            webGrid.Column(columnName: "Monto_extra", header: "Extrafinanciamiento"),
                            webGrid.Column(columnName: "Fecha_activación", header: "Fecha"),
                            webGrid.Column(columnName: "Internacional", header: "Internacional")
                            )).ToString();
            Response.ClearContent(); 
            Response.AddHeader("content-disposition", "attachment; filename=Tarjetas.xls"); 
            Response.ContentType = "application/excel"; 
            Response.Write(gridData);
            Response.End();
        }



        //Muestra todas las tarjetas registradas a las que se les puede dar mantenimiento
        public ActionResult AdminCards()
        {
            IEnumerable<Tarjeta> Cards = _oGestorTarjeta.ListadoTarjetas();
            return View(Cards);
        }


        //Para eliminar una tarjeta de la base de datos
        public ActionResult BorrarTarjeta(int Id)
        {
            int registro = _oGestorTarjeta.BorrarTarjeta(Id);
            return RedirectToAction("AdminCards");

        }


        //Muestra el formulario que permite modificar una tarjeta
        public ActionResult EditarTarjetaForm(int Id)
        {
            Tarjeta obj = _oGestorTarjeta.ListadoTarjetas().Where(x => x.Id == Id).FirstOrDefault();
            return View(obj);

        }


        //Para actualizar una tarjeta en los mantemientos
        public ActionResult ActualizarTarjeta(Tarjeta pTarjeta)
        {
            int registros = _oGestorTarjeta.ActualizarTarjeta(pTarjeta);
            return RedirectToAction("AdminCards");

        }

    }
}