using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.Traslado
{
    
    public class Ficha
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
        public string autoDepositoDestino { get; set; }
        public string codigoDepositoDestino { get; set; }
        public string nombreDepositoDestino { get; set; }
        public decimal cantidad { get; set; }
        public string decimales { get; set; }
        public string nota { get; set; }
        public string documentoNombre { get; set; }

    }

}