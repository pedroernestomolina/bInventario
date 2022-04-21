using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{

    public class FichaPrecio
    {

        public decimal precioNeto { get; set; }
        public decimal precio_divisa_full { get; set; }


        public FichaPrecio() 
        {
            precioNeto = 0m;
            precio_divisa_full = 0m;
        }

    }

}