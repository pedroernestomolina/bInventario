using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class FichaPrdPrecioHistorico
    {

        public string autoProducto { get; set; }
        public string nota { get; set; }
        public string precio_id { get; set; }
        public decimal precio { get; set; }


        public FichaPrdPrecioHistorico() 
        {
            autoProducto = "";
            nota = "";
            precio_id = "";
            precio = 0m;
        }

    }

}