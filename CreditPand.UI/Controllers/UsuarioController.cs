using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using CreditPand.BD.Repositorios;


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
            //Swal.fire("Registro Exitoso"); //Alert de registro exitoso
            return RedirectToAction("Register");

        }

        //Muestra el formulario para ingresar sesión
        public ActionResult Login()
        {
            return View();
        }


        //Permite el Login del usuario
        public ActionResult LoginUser(Usuario pUsuario) 
        {


            if (ModelState.IsValid) 
            {
                using (CreditPandEntities ContextoBD = new CreditPandEntities()) //No debería ir acá, solamente el if
                {
                   
                var data = ContextoBD.Usuario.Where(a => a.Username.Equals(pUsuario.Username) && 
                a.Pass.Equals(pUsuario.Pass)).ToList();

                
                if (data.Count() > 0)
                {
                    Session["Username"] = data.FirstOrDefault().Username;
                    return RedirectToAction("UserDashBoard");
                }
                else
                {
                    ViewBag.error = "Fallo";

                        MessageBox.Show("Usuario o contraseña incorrectos", "Intente denuevo");//Cambiar por SweetAlert
                        return RedirectToAction("Login");
                }

                }
            }
            return View();

        }

        //Muestra el nombre del usuario que ingreso en sesión, CAMBIAR
        public ActionResult UserDashBoard()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //Muestra el perfil del usuario que se encuentra en sesión en ese momento
        public ActionResult UserProfile(int id, Usuario pUsuario)
        {
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                var info = from Usuario in ContextoBD.Usuario
                           where pUsuario.Ide == id
                           select Usuario;
                var user = info.FirstOrDefault<Usuario>();
                return View(user);
            }
            
        }


        //Muestra la tabla con todos los clientes a los que se les puede dar mantenimiento
        public ActionResult Mantenimientos() {

            IEnumerable<Usuario> Clientes = _oGestorUsuario.ListadoUsuarios();
            return View(Clientes);

        }


        //********************************************************
        //Para crear un usuario en los mantenimientos, falta crear la vista del form para usarla
        public ActionResult CrearUsuario(Usuario pUsuario)
        {
            int registros = _oGestorUsuario.CrearUsuario(pUsuario);
            return RedirectToAction("Mantenimientos");
        }
        //********************************************************


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


        //Para la asignación de roles a los usuarios
        public ActionResult Roles()
        {
            IEnumerable<Usuario> Clientes = _oGestorUsuario.ListadoUsuarios();
            return View(Clientes);
        }

        
    }
}