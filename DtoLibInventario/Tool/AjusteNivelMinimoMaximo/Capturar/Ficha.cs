using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar
{
    
    public class Ficha
    {

        public string autoProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string referenciaProducto { get; set; }
        public string decimales { get; set; }
        public decimal fisica { get; set; }
        public decimal nivelMinimo { get; set; }
        public decimal nivelOptimo { get; set; }
        public string esPesado { get; set; }
        public string esSuspendido { get; set; }

    }

}