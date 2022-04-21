using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Kardex.Movimiento.Resumen
{
    
    public class Data
    {

        public string autoDeposito { get; set; }
        public string autoConcepto { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public string codigoConcepto { get; set; }
        public string nombreConcepto { get; set; }
        public int cntMovimiento { get; set; }
        public string modulo { get; set; }
        public decimal cntInventario { get; set; }
        public string siglas { get; set; }

    }

}