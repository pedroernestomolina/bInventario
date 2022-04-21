using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.CostoExistencia
{
    
    public class Ficha
    {

        private decimal costoDivisa { get; set; }
        private int contenidoCompras { get; set; }

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string autoDeposito { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public string autoDepart { get; set; }
        public string codigoDepart { get; set; }
        public string nombreDepart { get; set; }
        public decimal cntFisica { get; set; }
        public string decimales { get; set; }
        public decimal costoUnd { get; set; }
        public DateTime fechaUltActCosto { get; set; }
        public string esPesado { get; set; }
        public string esAdmDivisa { get; set; }
        public string estatusActivo { get; set; }
        public string estatusSuspendido { get; set; }

        public decimal costoDivisaUnd 
        {
            get 
            {
                var rt = 0.0m;
                rt = costoDivisa / contenidoCompras;
                return rt;
            }
        }


    }

}