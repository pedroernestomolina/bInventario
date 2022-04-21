using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.VerData
{
    
    public class Costo
    {

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string nombreTasaIva { get; set; }
        public decimal tasaIva { get; set; }
        public Enumerados.EnumAdministradorPorDivisa admDivisa { get; set; }
        public Enumerados.EnumEstatus estatus { get; set; }
        public string empaqueCompra { get; set; }
        public int contEmpaqueCompra { get; set; }

        public decimal costoProveedorUnd { get; set; }
        public decimal costoImportacionUnd { get; set; }
        public decimal costoVarioUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public DateTime? fechaUltCambio { get; set; }
        public int Edad { get; set; }

    }

}