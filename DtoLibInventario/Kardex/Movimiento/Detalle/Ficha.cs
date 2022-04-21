using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Kardex.Movimiento.Detalle
{
    
    public class Ficha
    {

        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string referenciaProducto { get; set; }
        public decimal existencia { get; set; }
        public int contenidoEmp { get; set; }
        public string EmpaqueCompra { get; set; }
        public string decimales { get; set; }
        public List<Data> Data { get; set; }

    }

}