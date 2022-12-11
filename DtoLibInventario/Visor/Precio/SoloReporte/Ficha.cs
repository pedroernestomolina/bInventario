using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.Precio.SoloReporte
{
    
    public class Ficha
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal tasa { get; set; }
        public decimal p1 { get; set; }
        public decimal p2 { get; set; }
        public decimal p3 { get; set; }
        public decimal p4 { get; set; }
        public decimal p1_FD { get; set; }
        public decimal p2_FD { get; set; }
        public decimal p3_FD { get; set; }
        public decimal p4_FD { get; set; }
        public decimal pM1 { get; set; }
        public decimal pM2 { get; set; }
        public decimal pM3 { get; set; }
        public decimal pM4 { get; set; }
        public decimal pM1_FD { get; set; }
        public decimal pM2_FD { get; set; }
        public decimal pM3_FD { get; set; }
        public decimal pM4_FD { get; set; }
        public decimal pDSP1 { get; set; }
        public decimal pDSP2 { get; set; }
        public decimal pDSP3 { get; set; }
        public decimal pDSP4 { get; set; }
        public decimal pDSP1_FD { get; set; }
        public decimal pDSP2_FD { get; set; }
        public decimal pDSP3_FD { get; set; }
        public decimal pDSP4_FD { get; set; }
        public int cont_emp_1 { get; set; }
        public int cont_emp_2 { get; set; }
        public int cont_emp_3 { get; set; }
        public string emp_1 { get; set; }
        public string emp_2 { get; set; }
        public string emp_3 { get; set; }
    }

}