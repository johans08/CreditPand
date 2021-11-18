using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditPand.BD.Repositorios
{
    public class GestorUsuario : IGestorUsuario
    {
       
        //Método para mostrar a todos los usuarios en la vista de mantenimientos
        IEnumerable<Usuario> IGestorUsuario.ListadoUsuarios()
        {
            List<Usuario> Clientes = new List<Usuario>();
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                Clientes= ContextoBD.Usuario.ToList();
            }

            return Clientes;

        }


        //Método para registrar a un nuevo usuario
        int IGestorUsuario.CrearUsuario(Usuario pUsuario)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                ContextoBD.Usuario.Add(pUsuario);
                n = ContextoBD.SaveChanges();
            }
            return n;

        }

        //Método para actualizar a un usuario en la vista de mantenimientos 
        int IGestorUsuario.ActualizarUsuario(Usuario pUsuario)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                ContextoBD.Entry<Usuario>(pUsuario).State = System.Data.Entity.EntityState.Modified;
                n = ContextoBD.SaveChanges();
            }
            return n;

        }

        
        //Método para borrar a un usuario en la vista de mantenimientos
        int IGestorUsuario.BorrarUsuario(string Username)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                var obj = ContextoBD.Usuario.Where(x => x.Username == Username).FirstOrDefault();
                if (obj == null)
                {
                    n = 0;
                }
                else
                {
                    ContextoBD.Entry<Usuario>(obj).State = System.Data.Entity.EntityState.Deleted;
                    n = ContextoBD.SaveChanges();
                }
            }
            return n;

        }










        //Para encontrar a una persona específica, para el perfil de usuario, QUITAR
        /*int Find(string Username)
        {
            IEnumerable<Tarjeta> cards = ListadoUsuarios();
            Tarjeta oTarjeta = Listado.Where(x => x.IdProducto == Id).FirstOrDefault();
            return auxProducto;
        }*/


        //Método para ingresar en sesión, QUITAR 
        public int Login(Usuario pUsuario)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                    var obj = ContextoBD.Usuario.Where(a => a.Username.Equals(pUsuario.Username) && a.Pass.Equals(pUsuario.Pass) && (a.Rol.Equals(pUsuario.Rol.Equals(1)) || a.Rol.Equals(pUsuario.Rol.Equals(2))) ).FirstOrDefault();
                
            }
            return n;
        }


        //Método para mostrar los datos del usuario que ingreso en sesión, QUITAR
        public int Profile(int id, Usuario pUsuario)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                var info = from Usuario in ContextoBD.Usuario
                             where pUsuario.Ide == id
                           select Usuario;
                var user = info.FirstOrDefault<Usuario>();
            }

            return n;
        }


    
}



    
}