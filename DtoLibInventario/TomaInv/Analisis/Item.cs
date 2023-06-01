using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.Analisis
{
    public class Item
    {
        public string idPrd { get; set; }
        public string codPrd { get; set; }
        public string descPrd { get; set; }
        public decimal fisico { get; set; }
        public decimal? conteo { get; set; }
        public decimal? vtas { get; set; }
        public decimal? comp { get; set; }
        public decimal? inv { get; set; }
        public decimal fisicoDep { get; set; }
    }
}