using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Kardex
{
    
    public class Existencia
    {

        public string autoPrd { get; set; }
        public decimal? exInicial { get; set; }


        public Existencia() 
        {
            autoPrd = "";
            exInicial = 0m;
        }

    }

}