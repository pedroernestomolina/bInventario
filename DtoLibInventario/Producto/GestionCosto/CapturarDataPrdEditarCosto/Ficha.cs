using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.GestionCosto.CapturarDataPrdEditarCosto
{
    public class Ficha
    {
        public string idPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string descPrd{ get; set; }
        public string descTasaIva { get; set; }
        public decimal porcTasaIva { get; set; }
        public string estatusDivisaPrd { get; set; }
        public string descEmqCompra { get; set; }
        public int contEmqCompra { get; set; }
        public decimal costoProvPrd { get; set; }
        public decimal costoImportacionPrd { get; set; }
        public decimal costoVarioPrd { get; set; }
        public decimal costoFinalPrd { get; set; }
        public decimal costoDivisaPrd { get; set; }
        public decimal costoPromedioPrd { get; set; }
        public DateTime fechaUltCambio { get; set; }
    }
}