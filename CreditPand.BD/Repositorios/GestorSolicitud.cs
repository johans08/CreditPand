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

        IEnumerable<Solicitud> IGestorSolicitud.ListadoSolicitud()
        {
            List<Solicitud> solicituds = new List<Solicitud>();
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                solicituds = ContextoBD.Solicitud.ToList();
            }

            return solicituds;
        }


        //Método para agregar una tarjeta si se acepta la solicitud
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


        //Método para borrar la infomración de una tarjeta en los mantenimientos
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
