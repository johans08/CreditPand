using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditPand.BD.Repositorios;
using CreditPand.BD.Modelo;

namespace CreditPand.BD.Interface
{
    public interface IGestorUsuario
    {

        IEnumerable<Usuario> ListadoUsuarios();

        int CrearUsuario(Usuario pUsuario);

        int ActualizarUsuario(Usuario pUsuario);

        int BorrarUsuario(string Username);

        //int Login(Usuario pUsuario); 

       // Usuario Profile(string Username, Usuario pUsuario);
    }
}
