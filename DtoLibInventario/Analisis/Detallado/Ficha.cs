using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Analisis.Detallado
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string codigoPrd { get; set; }
        public string decimales { get; set; }
        public int cntDoc { get; set; }
        public decimal cntUnd { get; set; }
        public DateTime fecha { get; set; }

    }

}