using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.DepositoResumen
{
    
    public class Ficha
    {

        public int cntStock { get; set; }
        public string autoDeposito { get; set; }
        public int cItem { get; set; }
        public string nombreDeposito { get; set; }
        public decimal costo { get; set; }
        public decimal pn1 { get; set; }
        public decimal pn2 { get; set; }
        public decimal pn3 { get; set; }
        public decimal pn4 { get; set; }
        public decimal pn5 { get; set; }
        public string codigoSuc { get; set; }
        public string nombreGrupo { get; set; }
        public string precioId { get; set; }

    }

}