using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Transito.Movimiento.Lista
{
    
    public class Filtro
    {


        public string codMov { get; set; }
        public string tipMov { get; set; }


        public Filtro() 
        {
            codMov = "";
            tipMov = "";
        }

    }

}