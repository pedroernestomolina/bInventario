using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Usuario
{
    
    public class Ficha
    {

        public string autoUsu { get; set; }
        public string autoGru { get; set; }
        public string nombreUsu { get; set; }
        public string apellidoUsu { get; set; }
        public string codigoUsu { get; set; }
        public string nombreGru { get; set; }
        private string estatusUsu { get; set; }
        public bool isActivo 
        {
            get 
            {
                var rt = false;
                if (estatusUsu.Trim().ToUpper() == "ACTIVO")
                    rt = true;
                return rt;
            }
        }

    }

}