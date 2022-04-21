using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Deposito
{
    
    public class Resumen
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string estatusActivo { get; set; }
        public string estatusPreDeterminado { get; set; }


        public Resumen() 
        {
            auto = "";
            codigo = "";
            nombre = "";
            estatusActivo = "";
            estatusPreDeterminado = "";
        }

    }

}