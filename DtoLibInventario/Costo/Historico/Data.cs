using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Costo.Historico
{
    
    public class Data
    {

        public string nota { get; set; }
        public string usuario { get; set; }
        public string estacion { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public decimal costo { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal tasaDivisa { get; set; }
        public string serie { get; set; }
        public string documento { get; set; }
        public Enumerados.enumModoCambio modoCambio { get; set; }

    }

}