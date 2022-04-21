using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Configuracion.MetodoCalculoUtilidad.Editar
{
    
    public class FichaPrecio
    {

        public string idProducto { get; set; }
        public Precio Precio_1 { get; set; }
        public Precio Precio_2 { get; set; }
        public Precio Precio_3 { get; set; }
        public Precio Precio_4 { get; set; }
        public Precio Precio_5 { get; set; }


        public FichaPrecio()
        {
            idProducto = "";
            Precio_1 = new Precio(); ;
            Precio_2 = new Precio(); ;
            Precio_3 = new Precio(); ;
            Precio_4 = new Precio(); ;
            Precio_5 = new Precio(); ;
        }

    }

}