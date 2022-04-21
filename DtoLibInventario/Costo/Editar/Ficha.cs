using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Costo.Editar
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public string estacion { get; set; }
        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public decimal costoProveedor { get; set; }
        public decimal costoProveedorUnd { get; set; }
        public decimal costoImportacion { get; set; }
        public decimal costoImportacionUnd { get; set; }
        public decimal costoVario { get; set; }
        public decimal costoVarioUnd { get; set; }
        public decimal costoFinal { get; set; }
        public decimal costoFinalUnd { get; set; }
        public decimal costoPromedio { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public decimal costoDivisa { get; set; }

        public FichaHistorica historia { get; set; }
        public FichaPrecio precio { get; set; }

    }

}