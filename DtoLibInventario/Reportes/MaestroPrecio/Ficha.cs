using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroPrecio
{
    
    public class Ficha
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string admDivisa { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public decimal tasa { get; set; }

        public decimal p1_neto  { get; set; }
        public decimal p2_neto { get; set; }
        public decimal p3_neto { get; set; }
        public decimal p4_neto { get; set; }
        public decimal p5_neto { get; set; }
        public int cont_1 { get; set; }
        public int cont_2 { get; set; }
        public int cont_3 { get; set; }
        public int cont_4 { get; set; }
        public int cont_5 { get; set; }
        public decimal p1_div_full { get; set; }
        public decimal p2_div_full { get; set; }
        public decimal p3_div_full { get; set; }
        public decimal p4_div_full { get; set; }
        public decimal p5_div_full { get; set; }
        public string empaque_1 { get; set; }
        public string empaque_2 { get; set; }
        public string empaque_3 { get; set; }
        public string empaque_4 { get; set; }
        public string empaque_5 { get; set; }

        public decimal pM1_neto { get; set; }
        public decimal pM2_neto { get; set; }
        public decimal pM3_neto { get; set; }
        public decimal pM4_neto { get; set; }
        public int cont_M1 { get; set; }
        public int cont_M2 { get; set; }
        public int cont_M3 { get; set; }
        public int cont_M4 { get; set; }
        public decimal pM1_div_full { get; set; }
        public decimal pM2_div_full { get; set; }
        public decimal pM3_div_full { get; set; }
        public decimal pM4_div_full { get; set; }
        public string empaque_M1 { get; set; }
        public string empaque_M2 { get; set; }
        public string empaque_M3 { get; set; }
        public string empaque_M4 { get; set; }

        public decimal pD1_neto { get; set; }
        public decimal pD2_neto { get; set; }
        public decimal pD3_neto { get; set; }
        public decimal pD4_neto { get; set; }
        public int cont_D1 { get; set; }
        public int cont_D2 { get; set; }
        public int cont_D3 { get; set; }
        public int cont_D4 { get; set; }
        public decimal pD1_div_full { get; set; }
        public decimal pD2_div_full { get; set; }
        public decimal pD3_div_full { get; set; }
        public decimal pD4_div_full { get; set; }
        public string empaque_D1 { get; set; }
        public string empaque_D2 { get; set; }
        public string empaque_D3 { get; set; }
        public string empaque_D4 { get; set; }
    }

}