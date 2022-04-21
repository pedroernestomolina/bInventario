using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Precio.Historico
{

    public class Resumen
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public List<Data> data { get; set; }
      
    }

}