using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Diagnostics;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditPand.BD.Repositorios
{
    public class GestorTarjeta : IGestorTarjeta
    {
       
        //Método para mostrar todas las solicitudes de tarjetas o tarjetas registradas
        IEnumerable<Tarjeta> IGestorTarjeta.ListadoTarjetas()
        {
            List<Tarjeta> Cards = new List<Tarjeta>();
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                Cards = ContextoBD.Tarjeta.ToList();
            }

            return Cards;
        }


        //Método para agregar una tarjeta si se acepta la solicitud
        int IGestorTarjeta.CrearTarjeta(Tarjeta pTarjeta)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                ContextoBD.Tarjeta.Add(pTarjeta);
                n = ContextoBD.SaveChanges();
            }
            return n;
        }


        //Método para borrar la infomración de una tarjeta en los mantenimientos
        int IGestorTarjeta.BorrarTarjeta(int pIdTarjeta)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                var obj = ContextoBD.Tarjeta.Where(x => x.Id == pIdTarjeta).FirstOrDefault();
                if (obj == null)
                {
                    n = 0;
                }
                else
                {
                    ContextoBD.Entry<Tarjeta>(obj).State = System.Data.Entity.EntityState.Deleted;
                    n = ContextoBD.SaveChanges();
                }
            }
            return n;
        }



        //Método para actualizar la información de un tarjeta en los mantenimientos
        int IGestorTarjeta.ActualizarTarjeta(Tarjeta pTarjeta)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                ContextoBD.Entry<Tarjeta>(pTarjeta).State = System.Data.Entity.EntityState.Modified;
                n = ContextoBD.SaveChanges();
            }
            return n;
        }



        //Método para actualizar los intereses de las tarjetas
        int IGestorTarjeta.ActualizarInteres(Interes objInteres)
        {
            int n = 0;
            using (CreditPandEntities ContextoBD = new CreditPandEntities())
            {
                ContextoBD.Entry<Interes>(objInteres).State = System.Data.Entity.EntityState.Modified;
                

                    n = ContextoBD.SaveChanges();
                
                    return n;
            }
        }
    }
}
