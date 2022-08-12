using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.EmpaqueMedida
{
    
    public class Resumen
    {

        public string auto { get; set; }
        public string nombre { get; set; }
        public string decimales { get; set; }


        public Resumen() 
        {
            auto = "";
            nombre = "";
            decimales = "";
        }

    }

}