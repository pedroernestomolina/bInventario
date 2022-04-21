using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Depositos.Asignar
{
    
    public class DepAsignar
    {

        public string autoDeposito { get; set; }
        public decimal fisica { get; set; }
        public decimal reservada { get; set; }
        public decimal disponible { get; set; }
        public decimal averia { get; set; }
        public string ubicacion_1 { get; set; }
        public string ubicacion_2 { get; set; }
        public string ubicacion_3 { get; set; }
        public string ubicacion_4 { get; set; }
        public decimal nivel_minimo { get; set; }
        public decimal nivel_optimo { get; set; }
        public decimal pto_pedido { get; set; }
        public DateTime fechaUltConteo { get; set; }
        public decimal resultadoUltConteo { get; set; }

    }

}