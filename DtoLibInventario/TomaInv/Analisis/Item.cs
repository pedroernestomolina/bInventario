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
        public decimal? cntVenta { get; set; }
        public decimal? cntCompra { get; set; }
        public decimal? cntMovInv { get; set; }
        public decimal? cntPorDespachar { get; set; }
        public decimal? exDeposito { get; set; }
    }
}