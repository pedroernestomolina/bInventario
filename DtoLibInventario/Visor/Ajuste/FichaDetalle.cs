using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.Ajuste
{
    
    public class FichaDetalle
    {

        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string autoUsuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string documentoNro { get; set; }
        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string autoDepositoOrigen { get; set; }
        public string codigoDepositoOrigen { get; set; }
        public string nombreDepositoOrigen { get; set; }
        public decimal cantidadUnd { get; set; }
        public decimal costoUnd { get; set; }
        public decimal importe { get; set; }
        public string decimales { get; set; }
        public string nota { get; set; }
        public int signo { get; set; }

    }

}