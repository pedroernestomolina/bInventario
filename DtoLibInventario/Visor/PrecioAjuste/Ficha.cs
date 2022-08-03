using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.PrecioAjuste
{
    
    public class Ficha
    {

        public string auto {get;set;}
        public string nombre  {get;set;}

        public int contEmp1_1 { get; set; }
        public int contEmp1_2 { get; set; }
        public int contEmp1_3 { get; set; }
        public int contEmp1_4 { get; set; }
        public int contEmp1_5 { get; set; }
        public int contEmp2_1 { get; set; }
        public int contEmp2_2 { get; set; }
        public int contEmp2_3 { get; set; }
        public int contEmp2_4 { get; set; }
        public int contEmp2_5 { get; set; }
        public int contEmp3_1 { get; set; }
        public int contEmp3_2 { get; set; }
        public int contEmp3_3 { get; set; }
        public int contEmp3_4 { get; set; }
        public int contEmp3_5 { get; set; }

        public decimal pFDivEmp1_1 { get; set; }
        public decimal pFDivEmp1_2 { get; set; }
        public decimal pFDivEmp1_3 { get; set; }
        public decimal pFDivEmp1_4 { get; set; }
        public decimal pFDivEmp1_5 { get; set; }
        public decimal pFDivEmp2_1 { get; set; }
        public decimal pFDivEmp2_2 { get; set; }
        public decimal pFDivEmp2_3 { get; set; }
        public decimal pFDivEmp2_4 { get; set; }
        public decimal pFDivEmp2_5 { get; set; }
        public decimal pFDivEmp3_1 { get; set; }
        public decimal pFDivEmp3_2 { get; set; }
        public decimal pFDivEmp3_3 { get; set; }
        public decimal pFDivEmp3_4 { get; set; }
        public decimal pFDivEmp3_5 { get; set; }


        public Ficha() 
        {
            auto = "";
            nombre = "";
            //
            contEmp1_1 = 0;
            contEmp1_2 = 0;
            contEmp1_3 = 0;
            contEmp1_4 = 0;
            contEmp1_5 = 0;
            contEmp2_1 = 0;
            contEmp2_2 = 0;
            contEmp2_3 = 0;
            contEmp2_4 = 0;
            contEmp2_5 = 0;
            contEmp3_1 = 0;
            contEmp3_2 = 0;
            contEmp3_3 = 0;
            contEmp3_4 = 0;
            contEmp3_5 = 0;
            //
            pFDivEmp1_1 = 0m;
            pFDivEmp1_2 = 0m;
            pFDivEmp1_3 = 0m;
            pFDivEmp1_4 = 0m;
            pFDivEmp1_5 = 0m;
            pFDivEmp2_1 = 0m;
            pFDivEmp2_2 = 0m;
            pFDivEmp2_3 = 0m;
            pFDivEmp2_4 = 0m;
            pFDivEmp2_5 = 0m;
            pFDivEmp3_1 = 0m;
            pFDivEmp3_2 = 0m;
            pFDivEmp3_3 = 0m;
            pFDivEmp3_4 = 0m;
            pFDivEmp3_5 = 0m;
        }

    }

}