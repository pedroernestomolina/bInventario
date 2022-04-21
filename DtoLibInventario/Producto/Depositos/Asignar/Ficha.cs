using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Depositos.Asignar
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public List<DepAsignar> depAsignar { get; set; }
        public List<DepRemover> depRemover { get; set; }

    }

}