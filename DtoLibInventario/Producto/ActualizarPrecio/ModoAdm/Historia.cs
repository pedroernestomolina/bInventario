using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.ActualizarPrecio.ModoAdm
{
    public class Historia
    {
        public string nota { get; set; }
        public string precio_id { get; set; }
        public decimal precio { get; set; }
        public string empaque { get; set; }
        public int contenido { get; set; }
        public decimal factorCambio { get; set; }
    }
}