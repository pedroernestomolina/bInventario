using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.EntradaxCompra
{
    
    public class Ficha
    {
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string nroDoc { get; set; }
        public string entidadProv { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string codDeposito { get; set; }
        public string descDeposito { get; set; }
        public decimal cantUnd { get; set; }
        public int signoDoc { get; set; }
        public string siglasDoc { get; set; }
        public string codConcepto { get; set; }
        public string descConcepto { get; set; }
    }

}