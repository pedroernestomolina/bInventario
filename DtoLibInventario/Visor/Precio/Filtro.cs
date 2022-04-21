using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.Precio
{
    
    public class Filtro
    {

        public string autoDepart { get; set; }
        public string autoGrupo{ get; set; }
        public string tipoPrecio { get; set; }


        public Filtro()
        {
            autoDepart = "";
            autoGrupo = "";
            tipoPrecio = "";
        }

    }

}