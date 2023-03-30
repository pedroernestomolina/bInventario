using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarPrecio.ModoAdm
{
    public class Historia
    {
        public string empDesc { get; set; }
        public int  empCont { get; set; }
        public decimal netoMonLocal { get; set; }
        public decimal fullDivisa { get; set; }
        public string tipoEmpaqueVenta { get; set; }
        public string tipoPrecioVenta { get; set; }
    }
}