﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditPand.BD.Repositorios;
using CreditPand.BD.Modelo;

namespace CreditPand.BD.Interface
{
    public interface IGestorTarjeta
    {
        
            IEnumerable<Tarjeta> ListadoTarjetas();

            int CrearTarjeta(Tarjeta pTarjeta);

            int ActualizarTarjeta(Tarjeta pTarjeta);

            int BorrarTarjeta(int pIdTarjeta);
        
            int ActualizarInteres(Interes objInteres);
    }
}
