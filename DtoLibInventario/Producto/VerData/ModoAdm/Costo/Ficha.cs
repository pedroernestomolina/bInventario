using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData.ModoAdm.Costo
{
    public class Ficha
    {
        public string auto { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal tasaIva { get; set; }
        public decimal costoMonedaDivisa { get; set; }
        public decimal costoMonedaLocal { get; set; }
        public string estatusDivisa { get; set; }
        public string autoEmpCompra  { get; set; }
        public string descEmpCompra { get; set; }
        public int contEmpCompra { get; set; }
    }
}