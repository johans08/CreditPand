using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditPand.BD.Repositorios;
using CreditPand.BD.Modelo;

namespace CreditPand.BD.Interface
{
    public interface IGestorSolicitud
    {

        IEnumerable<Solicitud> ListadoSolicitud();

        int CrearSolicitud(Solicitud pSolicitud);

        int BorrarSolicitud(int pSolicitud);
    }
}
