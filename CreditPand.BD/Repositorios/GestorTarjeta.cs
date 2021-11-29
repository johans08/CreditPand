using CreditPand.BD.Interface;
using CreditPand.BD.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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


        //Método para agregar una tarjeta de forma directa
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


        //Método para borrar la información de una tarjeta en los mantenimientos
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


        //Método para aprobar, cuando la tarjeta no se realiza de manera directa
        int IGestorTarjeta.Aprobar(Solicitud pSolicitud, Tarjeta oTarjeta, int id,
            string Marca, int Límite, int Monto_extra, System.DateTime Fecha_activación, Boolean Internacional, int IdUsuario)
        {
            int n = 0;

           /* using (CreditPandEntities ContextoBD = new CreditPandEntities())

                ContextoBD.Tarjeta.Add(pSolicitud);
                n = ContextoBD.SaveChanges();
            }
        }*/

            string CadenaConexion;
            CadenaConexion = ConfigurationManager.ConnectionStrings["CreditPandEntities"].ConnectionString.Split('"')[1];
            using (SqlConnection objConexion = new SqlConnection(CadenaConexion))

            {
                // ContextoBD.Tarjeta.Add();

                

                SqlCommand objComando = new SqlCommand();
                objComando.Connection = objConexion;
                objComando.CommandType = System.Data.CommandType.Text;
                objComando.CommandText = "Insert into Tarjeta (Marca, Límite, Monto_extra, Fecha_activación, Internacional ,IdUsuario)" +
                                          "Values (@Marca, @Límite, @Monto_extra, @Fecha_activación, @Internacional, @IdUsuario)";
                                         //"Select Marca, Límite, Monto_extra, Fecha_activación, Internacional ,IdUsuario from Solicitud";
                //"Values (@NomProducto,@MarcaProducto,@CostoProducto)";



                objComando.Parameters.Add(new SqlParameter("@Id", id));
                objComando.Parameters.Add(new SqlParameter("@Marca", Marca));

                objComando.Parameters.Add(new SqlParameter("@Límite", Límite));
                objComando.Parameters.Add(new SqlParameter("@Monto_extra", Monto_extra));

                SqlParameter oParametro2 = new SqlParameter();
                oParametro2.ParameterName = "@Fecha_activación";
                oParametro2.SqlDbType = System.Data.SqlDbType.DateTime;
                oParametro2.Value = Fecha_activación;
                objComando.Parameters.Add(oParametro2);

                objComando.Parameters.Add(new SqlParameter("@Internacional", Internacional));
                objComando.Parameters.Add(new SqlParameter("@IdUsuario", IdUsuario));

                objConexion.Open();
                n = objComando.ExecuteNonQuery();
                objConexion.Close();

            }

            return n;
        }

    }
}
