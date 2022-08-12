using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Tool.CambioMasivoPrecio
{
    
    abstract public class BaseData
    {

        public string autoPrd { get; set; }
        public string autoPrecioEmp_1 { get; set; }
        public string autoPrecioEmp_2 { get; set; }
        public string autoPrecioEmp_3 { get; set; }
        public decimal pNetoEmp_1 { get; set; }
        public decimal pNetoEmp_2 { get; set; }
        public decimal pNetoEmp_3 { get; set; }
        public int contEmp_1 { get; set; }
        public int contEmp_2 { get; set; }
        public int contEmp_3 { get; set; }
        public decimal utEmp_1 { get; set; }
        public decimal utEmp_2 { get; set; }
        public decimal utEmp_3 { get; set; }
        public decimal pFullDivEmp_1 { get; set; }
        public decimal pFullDivEmp_2 { get; set; }
        public decimal pFullDivEmp_3 { get; set; }

    }

}