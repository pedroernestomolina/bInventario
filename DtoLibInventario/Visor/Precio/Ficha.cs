using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.Precio
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string codigoDep { get; set; }
        public string nombreDep { get; set; }
        public string codigoGrupo { get; set; }
        public string nombreGrupo { get; set; }
        public string estatus { get; set; }
        public string estatusDivisa { get; set; }
        public DateTime fechaUltCosto { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public int contEmpCompra { get; set; }
        public decimal precio_1 { get; set; }
        public decimal precio_2 { get; set; }
        public decimal precio_3 { get; set; }
        public decimal precio_4 { get; set; }
        public decimal precio_5 { get; set; }


        public Ficha()
        {
            autoPrd = "";
            codigoPrd = "";
            nombrePrd = "";
            codigoDep= "";
            nombreDep= "";
            codigoGrupo = "";
            nombreGrupo = "";
            estatus= "";
            estatusDivisa = "";
            fechaUltCosto= DateTime.Now.Date;
            costoUnd = 0.0m;
            costoDivisa = 0.0m;
            contEmpCompra = 0;
            precio_1 = 0.0m;
            precio_2 = 0.0m;
            precio_3 = 0.0m;
            precio_4 = 0.0m;
            precio_5 = 0.0m;
        }

    }

}