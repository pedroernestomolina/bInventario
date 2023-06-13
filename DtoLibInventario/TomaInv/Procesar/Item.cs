using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.Procesar
{
    public class Item
    {
        public string idProducto { get; set; }
        public decimal diferencia { get; set; }
        public int signo { get; set; }
        public string estadoDesc { get; set; }
        public string estatusDivisa { get; set; }
        public decimal costoMonDivisa { get; set; }
        public decimal costoMonLocal { get; set; }
        public int contEmpCompra { get; set; }
    }
}
