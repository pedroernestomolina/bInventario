using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Depositos.Editar
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public string autoDeposito { get; set; }

        public string ubicacion_1 { get; set; }
        public string ubicacion_2 { get; set; }
        public string ubicacion_3 { get; set; }
        public string ubicacion_4 { get; set; }
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        public decimal ptoPedido { get; set; }

    }

}