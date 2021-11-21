using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditPand.BD.Repositorios
{
    public class GestorSolicitud : IGestorSolicitud
    {
        //Lista todas las solicitudes a tarjetas
        IEnumerable<Solicitud> IGestorSolicitud.ListadoSolicitud()
        {
            List<Solicitud> solicituds = new List<Solicitud>();
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                solicituds = ContextoBD.Solicitud.ToList();
            }

            return solicituds;
        }


        //Método para enviar una solicitud para una tarjeta
        int IGestorSolicitud.CrearSolicitud(Solicitud pSolicitud)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                ContextoBD.Solicitud.Add(pSolicitud);
                n = ContextoBD.SaveChanges();

            }
            return n;
        }


        //Método para borrar una solicitud, en caso de que sea denegada
        int IGestorSolicitud.BorrarSolicitud(int pIdSolicitud)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                var obj = ContextoBD.Solicitud.Where(x => x.Id == pIdSolicitud).FirstOrDefault();
                if (obj == null)
                {
                    n = 0;
                }
                else
                {
                    ContextoBD.Entry<Solicitud>(obj).State = System.Data.Entity.EntityState.Deleted;
                    n = ContextoBD.SaveChanges();
                }
            }
            return n;
        }
    }
}
