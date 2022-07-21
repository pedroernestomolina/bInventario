using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.ResumenCostoInv
{
    
    public class PorVentas
    {

        public string auto { get; set; }
        public decimal cntUnd { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal ventaDivisa { get; set; }
        public string siglas { get; set; }


        public PorVentas() 
        {
            auto = "";
            cntUnd = 0m;
            costoDivisa = 0m;
            ventaDivisa = 0m;
            siglas = "";
        }

    }

}
