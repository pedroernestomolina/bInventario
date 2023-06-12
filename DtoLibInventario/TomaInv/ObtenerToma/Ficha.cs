using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.ObtenerToma
{
    public class Ficha
    {
        public string idPrd { get; set; }
        public string codigoPrd { get; set; }
        public string descPrd { get; set; }
        public decimal? costoPrd { get; set; }
        public decimal? margen { get; set; }
        public decimal? cnt { get; set; }
        public decimal? cntMov { get; set; }
    }
}