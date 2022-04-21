using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Top20
{
    
    public class Ficha
    {

        public decimal cntUnd { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public string decimales { get; set; }
        public int  cntDoc { get; set; }
        private string estatusPesado { get; set; }
        public bool esPesado 
        {
            get 
            {
                var rt = false;
                if (estatusPesado == "1")
                    rt = true;
                return rt;
            }
        }

    }

}