using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class FichaPrdPrecio
    {

        public string autoProducto { get; set; }
        public FichaPrecio precio_1 { get; set; }
        public FichaPrecio precio_2 { get; set; }
        public FichaPrecio precio_3 { get; set; }
        public FichaPrecio precio_4 { get; set; }
        public FichaPrecio precio_5 { get; set; }


        public FichaPrdPrecio() 
        {
            autoProducto = "";
            precio_1 = new FichaPrecio();
            precio_2 = new FichaPrecio();
            precio_3 = new FichaPrecio();
            precio_4 = new FichaPrecio();
            precio_5 = new FichaPrecio();
        }

    }

}