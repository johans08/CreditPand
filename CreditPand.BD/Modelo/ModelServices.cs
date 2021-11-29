using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditPand.BD.Modelo
{
    public class ModelServices : IDisposable
    {
        private readonly CreditPandEntities _datos = new CreditPandEntities();

        public void Dispose()
        {
            _datos.Dispose();
        }


        public IEnumerable<Tarjeta> ObtenerTarjeta()
        {
            return _datos.Tarjeta.ToList();
        }


    }
}
