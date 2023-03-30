using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroPrecio.ModoAdm
{
    public class Ficha
    {
        public string prdCodigo { get; set; }
        public string prdNombre { get; set; }
        public string estatusDivisa { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public decimal tasaIva { get; set; }
        //
        public decimal netoMonLocal { get; set; }
        public int empCont { get; set; }
        public decimal fullDivisa { get; set; }
        public string empDesc { get; set; }
        public string tipoEmpVta  { get; set; }
    }
}