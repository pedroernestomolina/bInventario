using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Costo.Editar
{
    
    public class FichaHistorica
    {

        public decimal costo { get; set; }
        public decimal divisa { get; set; }
        public decimal tasaCambio { get; set; }
        public string serie { get; set; }
        public string documento { get; set; }
        public string nota { get; set; }

    }

}