using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Agregar
{
    public class HndPrecioVenta
    {
        public int idHndTipoPrecio { get; set; }
        public string tipoEmp { get; set; }
        public decimal netoMonedaLocal { get; set; }
        public decimal fullDivisa { get; set; }
        public decimal utilidadPorc { get; set; }
        public DateTime  ofertaDesde { get; set; }
        public DateTime ofertaHasta { get; set; }
        public decimal ofertaPorc { get; set; }
        public string ofertaEstatus { get; set; }
    }
}