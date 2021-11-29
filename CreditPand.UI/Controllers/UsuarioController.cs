using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using CreditPand.BD.Repositorios;
using PagedList;

namespace CreditPand.UI.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly IGestorUsuario _oGestorUsuario;

        //Constructor del Usuario
        public UsuarioController() 
        {
            _oGestorUsuario = new GestorUsuario();
        }


        // GET: Usuario
        //Muestra el formulario para el registro de un cliente
        public ActionResult Register() 
        {
            ViewBag.Message = "One Page transform.";

            return View();
        }


        //Realiza el registro del cliente en la BD
        public ActionResult RegistroUsuario(Usuario pUsuario)
        {
            int registros = _oGestorUsuario.CrearUsuario(pUsuario);
            return RedirectToAction("Register");

        }


        //Muestra el formulario para ingresar sesión
        public ActionResult Login()
        {
            return View();
        }



        //Permite el Login del usuario y del administrador
        public ActionResult LoginUser(Usuario pUsuario)
        {
            if (ModelState.IsValid)
            {
                using (CreditPandEntities ContextoBD = new CreditPandEntities()) //No debería ir acá, solamente el if
                {
                    var data = ContextoBD.Usuario.Where(a => a.Username.Equals(pUsuario.Username) &&
                    a.Pass.Equals(pUsuario.Pass) && a.Rol.Equals(pUsuario.Rol)).ToList();

                    var data2 = ContextoBD.Usuario.Where(a => a.Username.Equals(pUsuario.Username) &&
                    a.Pass.Equals(pUsuario.Pass) && a.Rol.Equals(pUsuario.Rol)).ToList();

                    
                    Session["Admin"] = null;
                    Session["Username"] = null;

                    



                    if (data.Count() > 0 && pUsuario.Rol.Equals(1))
                    {
                        Session["Username"] = data.FirstOrDefault().Username;
                        
                        //Para los datos del perfil del usuario y admin
                        Session["Ide"]=data.FirstOrDefault().Ide;
                        Session["Nombre"] = data.FirstOrDefault().Nombre;
                        Session["Apellido"] = data.FirstOrDefault().Apellido;
                        Session["SegundoApellido"] = data.FirstOrDefault().SegundoApellido;
                        Session["Telefono"] = data.FirstOrDefault().Telefono;
                        Session["Email"] = data.FirstOrDefault().Email;
                        Session["Pass"] = data.FirstOrDefault().Pass;

                        return RedirectToAction("Index", "Home");

                    }
                    else if (data.Count() > 0 && pUsuario.Rol.Equals(2))
                    {

                        Session["Admin"] = data.FirstOrDefault().Username;

                        //Para los datos del perfil del usuario y admin
                        Session["Ide"] = data.FirstOrDefault().Ide;
                        Session["Nombre"] = data.FirstOrDefault().Nombre;
                        Session["Apellido"] = data.FirstOrDefault().Apellido;
                        Session["SegundoApellido"] = data.FirstOrDefault().SegundoApellido;
                        Session["Telefono"] = data.FirstOrDefault().Telefono;
                        Session["Email"] = data.FirstOrDefault().Email;
                        Session["Pass"] = data.FirstOrDefault().Pass;


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }


                }
            }
            return View();

        }


        //Permite eliminar de sesión a un usuario y regresarlo al Login
        public ActionResult Logout()
        {
            Session.Remove("Username");
            Session.Remove("Admin");
            return RedirectToAction("Login");
        }



        //Muestra el perfil del usuario que se encuentra en sesión en ese momento
        public ActionResult User()
        {
                return View();
        }



        //Muestra el perfil del admin que está en sesión en ese momento
        public ActionResult AdminProfile()
        {
            return View();
        }



        //Muestra la tabla con todos los clientes a los que se les puede dar mantenimiento
        public ActionResult Mantenimientos(string sortOrder, string currentFilter, string searchString, int? page) {

            /*ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;*/

            IEnumerable<Usuario> Clientes = _oGestorUsuario.ListadoUsuarios();

            //int pageSize = 3;
           // int pageNumber = (page ?? 1);.ToPagedList(pageNumber, pageSize

            return View(Clientes);

        }




        //********************************************************
        //Para crear un usuario en los mantenimientos, falta crear la vista del form para usarla
        /*public ActionResult CrearUsuario(Usuario pUsuario)
        {
            int registros = _oGestorUsuario.CrearUsuario(pUsuario);
            return RedirectToAction("Mantenimientos");
        }*/




        //********************************************************
        //Para modificar el perfil de un usuario o un admin
        public ActionResult ModificarUserProfile(Usuario pUsuario)
        {
            int registros = _oGestorUsuario.ActualizarUsuario(pUsuario);
            return RedirectToAction("Index", "Home");
        }



        //Muestra el formulario que permite modificar a un usuario
        public ActionResult EditarUserForm(int Id)
        {
            Usuario obj = _oGestorUsuario.ListadoUsuarios().Where(x => x.Ide == Id).FirstOrDefault();
            return View(obj);

        }


        //Para modificar un usuario en los mantenimientos
        public ActionResult ModificarUsuario(Usuario pUsuario)
        {
            
            int registros = _oGestorUsuario.ActualizarUsuario(pUsuario);
            return RedirectToAction("Mantenimientos");

        }


        //Para borrar un usuario en los mantemientos
        public ActionResult BorrarUsuario(string Username)
        {
            int registro = _oGestorUsuario.BorrarUsuario(Username);
            return RedirectToAction("Mantenimientos");

        }


        //Para la asignación de roles a los usuarios, ¿QUITAR? LOS ROLES SE PUEDEN ASIGNAR DESDE EL UPDATE DEL USUARIO
        public ActionResult Roles()
        {
            IEnumerable<Usuario> Clientes = _oGestorUsuario.ListadoUsuarios();
            return View(Clientes);
        }

        
    }

}