using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.ResumenCostoInv
{
    
    public class PorInventario
    {

        public string autoPrd { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public decimal contEmpPrd { get; set; }
        public decimal? exIniUnd { get; set; }
        public decimal? costoIniEmpDivisa { get; set; }


        public PorInventario() 
        {
            autoPrd = "";
            codigoPrd = "";
            nombrePrd = "";
            contEmpPrd = 0m;
            exIniUnd = 0m;
            costoIniEmpDivisa = 0m;
        }

    }

}