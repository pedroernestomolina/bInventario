using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarPrecio.ModoAdm
{
    public class Precio
    {
        public int id { get; set; }
        public decimal utilidad { get; set; }
        public decimal netoMonedaLocal { get; set; }
        public decimal fullDivisa { get; set; }
    }
}