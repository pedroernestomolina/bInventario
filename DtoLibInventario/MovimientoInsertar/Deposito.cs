using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoInsertar
{
    public class Deposito
    {
        public string autoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string autoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public decimal cantidadUnd { get; set; }
        public Deposito()
        {
            autoProducto = "";
            autoDeposito = "";
            nombreProducto = "";
            nombreDeposito = "";
            cantidadUnd = 0m;
        }
    }
}