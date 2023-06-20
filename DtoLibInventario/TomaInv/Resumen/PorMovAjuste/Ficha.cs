using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.Resumen.PorMovAjuste
{
    public class Ficha
    {
        public string idToma { get; set; }
        public DateTime fechaEmision { get; set; }
        public string docTomaNro { get; set; }
        public string docSolicitudNro { get; set; }
        public int cntItemResult { get; set; }
    }
}