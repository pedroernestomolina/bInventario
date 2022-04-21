using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Kardex.Movimiento.Resumen
{
    
    public class Ficha
    {

        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string referenciaProducto { get; set; }
        public decimal existenciaActual { get; set; }
        public decimal existencaFecha { get; set; }
        public string fecha { get; set; }
        public int contenidoEmp  { get; set; }
        public string empaqueCompra { get; set; }
        public string decimales { get; set; }
        public List<Data> Data { get; set; }

    }

}
