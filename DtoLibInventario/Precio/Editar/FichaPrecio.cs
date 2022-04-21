using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibInventario.Precio.Editar
{

    public class FichaPrecio
    {

        public string autoEmp { get; set; }
        public int contenido { get; set; }
        public decimal utilidad { get; set; }
        public decimal precioNeto { get; set; }
        public decimal precio_divisa_Neto { get; set; }


        public FichaPrecio()
        {
            autoEmp = "";
            contenido = 0;
            utilidad = 0.0m;
            precioNeto = 0.0m;
            precio_divisa_Neto = 0.0m;
        }

    }

}