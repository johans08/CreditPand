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
        private readonly IGestorSolicitud _oGestorSolicitud;


        //Constructor de la tarjeta
        public TarjetaController()
        {
            _oGestorTarjeta = new GestorTarjeta();
            _oGestorSolicitud = new GestorSolicitud();
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
            MessageBox.Show("Intereses cambiados");//Cambiar por el sweetalert
            return RedirectToAction("InteresesForm");
        }



        //Formulario para la solicitud de una tarjeta de crédito por parte de un cliente
        public ActionResult ClientCards()
        {
            return View();
        }



        //Para enviar la solicitud de una tarjeta de crédito por parte de un cliente
        public ActionResult EnviarSolicitud(Solicitud pSolicitud)
        {
            int registros = _oGestorSolicitud.CrearSolicitud(pSolicitud);
            return RedirectToAction("ClientCards");
        }


        //Vista que muestra las solicitudes de tarjetas 
        public ActionResult SolicitudesTarjetas()
        {
            IEnumerable<Solicitud> Solicitudes = _oGestorSolicitud.ListadoSolicitud();
            return View(Solicitudes);
        }



        //Para eliminar una solicitud, si está no puede aprobarse
        public ActionResult BorrarSolicitud(int Id)
        {
            int registro = _oGestorSolicitud.BorrarSolicitud(Id);
            return RedirectToAction("SolicitudesTarjetas");

        }


        //Para aprobar la solicitud de una tarjeta de crédito
        public ActionResult AprobarSolicitud(Solicitud pSolicitud, Tarjeta oTarjeta, int id, 
            string Marca, int Límite, int Monto_extra, System.DateTime Fecha_activación, Boolean Internacional, int IdUsuario, FormCollection collection)
        {
            oTarjeta.Marca = collection["Marca"];
            oTarjeta.Límite = Convert.ToInt32(collection["Límite"]);
            oTarjeta.Monto_extra = Convert.ToInt32(collection["Monto_extra"]);
            oTarjeta.Fecha_activación = Convert.ToDateTime(collection["Fecha_activación"]);
            oTarjeta.Internacional = Convert.ToBoolean(Convert.ToInt16(collection["IdUsuario"]));
            oTarjeta.IdUsuario = Convert.ToInt32(collection["IdUsuario"]);

            int resulta =_oGestorTarjeta.Aprobar(pSolicitud, oTarjeta, id, Marca, Límite, Monto_extra, Fecha_activación, Internacional, IdUsuario);
            int registro = _oGestorSolicitud.BorrarSolicitud(id);

            return RedirectToAction("SolicitudesTarjetas");

        }


        public ActionResult AprobarForm(int? id)
        {

            Solicitud obj = _oGestorSolicitud.ListadoSolicitud().Where(x => x.Id == id).FirstOrDefault();
            return View(obj);

        }


        //Para registrar una tarjeta de credito de forma directa , QUITAR?????
        /*public ActionResult RegistroTarjeta(Tarjeta pTarjeta)
        {
            int registros = _oGestorTarjeta.CrearTarjeta(pTarjeta);
            return RedirectToAction("ClientCards");
        }*/














        //Muestra todas las tarjetas registradas que pueden analizar los clientes, PENDIENTE*****
        public ActionResult ConsultCards(DateTime? Fecha_activación = null, string Marca = null, int? Límite = null,string buscar=null)
        {

            /* int _TotalRegistros = 0;
             int _TotalPaginas = 0;

             // FILTRO DE BÚSQUEDA
             using (_DbContext = new AppDbContext())
             {
                 // Recuperamos el 'DbSet' completo
                 _Customers = _DbContext.Customers.ToList();

                 // Filtramos el resultado por el 'texto de búqueda'
                 if (!string.IsNullOrEmpty(buscar))
                 {
                     foreach (var item in buscar.Split(new char[] { ' ' },
                              StringSplitOptions.RemoveEmptyEntries))
                     {
                         _Customers = _Customers.Where(x => x.ContactName.Contains(item) ||
                                                       x.CompanyName.Contains(item) ||
                                                       x.Email.Contains(item))
                                                       .ToList();
                     }
                 }
             }

             // SISTEMA DE PAGINACIÓN
             using (_DbContext = new AppDbContext())
             {
                 // Número total de registros de la tabla Customers
                 _TotalRegistros = _Customers.Count();
                 // Obtenemos la 'página de registros' de la tabla Customers
                 _Customers = _Customers.OrderBy(x => x.ContactName)
                                                  .Skip((pagina - 1) * _RegistrosPorPagina)
                                                  .Take(_RegistrosPorPagina)
                                                  .ToList();
                 // Número total de páginas de la tabla Customers
                 _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

                 // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
                 _PaginadorCustomers = new PaginadorGenerico<Customer>()
                 {
                     RegistrosPorPagina = _RegistrosPorPagina,
                     TotalRegistros = _TotalRegistros,
                     TotalPaginas = _TotalPaginas,
                     PaginaActual = pagina,
                     BusquedaActual = buscar,
                     Resultado = _Customers
                 };
             }

             // Enviamos a la Vista la 'Clase de paginación'
             return View(_PaginadorCustomers);*/

            /*  var tarjetas = _GestorTarjeta.ObtenerPaginaDePersonasFiltrada(page, PERSONAS_POR_PAGINA,
                                     sort, sortDir, buscar, minHijos, maxHijos);
            */

            
                IEnumerable<Tarjeta> Cards = _oGestorTarjeta.ListadoTarjetas();


            return View(Cards);




        }
        

        //Método para buscar en el Grid
        public void Buscar(Tarjeta pTarjeta, DateTime? Fecha_activación = null, string Marca = null, int? Límite = null, string buscar = null)
        {
            using (CreditPandEntities ContextoBD = new CreditPandEntities()) //No debería ir acá, solamente el if
            {
                var bFecha = ContextoBD.Tarjeta.Where(a => a.Fecha_activación.Equals(pTarjeta.Fecha_activación)).ToList();
                var bMarca = ContextoBD.Tarjeta.Where(a => a.Marca.Equals(pTarjeta.Marca)).ToList();
                var bLimite = ContextoBD.Tarjeta.Where(a => a.Límite.Equals(pTarjeta.Límite)).ToList();



            }
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