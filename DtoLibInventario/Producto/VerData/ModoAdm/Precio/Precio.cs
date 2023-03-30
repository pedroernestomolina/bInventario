using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData.ModoAdm.Precio
{
    public class Precio
    {
        public int idHndPrecioVenta { get; set; }
        public int idHndEmpresaPrecio { get; set; }
        public int idHndEmpVenta { get; set; }
        public string autoEmp { get; set; }
        public int contEmp { get; set; }
        public string descEmp { get; set; }
        public decimal utEmp { get; set; }
        public decimal pnEmp { get; set; }
        public decimal pfdEmp { get; set; }
        public string ofertaEstatus { get; set; }
        public DateTime ofertaDesde { get; set; }
        public DateTime ofertaHasta { get; set; }
        public decimal ofertaPorct { get; set; }
        public string tipoEmp { get; set; }
    }
}