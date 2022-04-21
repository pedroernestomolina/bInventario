using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Depositos.Lista
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string descripcionPrd { get; set; }
        public string referenciaPrd { get; set; }
        public List<Data> depositos { get; set; } 

    }

}