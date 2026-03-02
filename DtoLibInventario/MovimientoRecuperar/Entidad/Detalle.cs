using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.MovimientoRecuperar.Entidad
{
    public class Detalle
    {
        public string prdCodigo{ get; set; }
        public string prdDesc { get; set; }
        public decimal cantidadEmp { get; set; }
        public decimal cantidadUnd { get; set; }
        public string empqDesc { get; set; }
        public int empqContenido { get; set; }
        public int signoMov { get; set; }
        public decimal costoUndMonedaLocal { get; set; }
        public decimal importeMonedaLocal { get; set; }
        public decimal costoUndMonedaRef { get; set; }
        public decimal importeMonedaRef { get; set; }
        public string cntDecimales { get; set; }
    }
}