using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class FichaPrdPrecioMargen
    {

        public string autoProducto { get; set; }
        public FichaPrecioMargen precio_1 { get; set; }
        public FichaPrecioMargen precio_2 { get; set; }
        public FichaPrecioMargen precio_3 { get; set; }
        public FichaPrecioMargen precio_4 { get; set; }
        public FichaPrecioMargen precio_5 { get; set; }


        public FichaPrdPrecioMargen() 
        {
            autoProducto = "";
            precio_1 = new FichaPrecioMargen();
            precio_2 = new FichaPrecioMargen();
            precio_3 = new FichaPrecioMargen();
            precio_4 = new FichaPrecioMargen();
            precio_5 = new FichaPrecioMargen();
        }

    }

}