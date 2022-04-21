using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Depositos.Ver
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string referenciaProducto { get; set; }
        public int contenidoCompra { get; set; }
        public string empaqueCompra { get; set; }

        public string autoDeposito { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }

        public decimal fisica { get; set; }
        public decimal reservada { get; set; }
        public decimal disponible { get; set; }
        public decimal averia { get; set; }
        public string ubicacion_1 { get; set; }
        public string ubicacion_2 { get; set; }
        public string ubicacion_3 { get; set; }
        public string ubicacion_4 { get; set; }
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        public decimal ptoPedido { get; set; }
        public DateTime? fechaUltConteo { get; set; }
        public decimal resultadoUltConteo { get; set; }

    }

}