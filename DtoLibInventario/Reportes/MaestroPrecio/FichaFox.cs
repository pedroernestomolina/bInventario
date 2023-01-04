using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroPrecio
{
    public class FichaFox
    {
        public string autoPrd { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string Depart { get; set; }
        public string Grupo { get; set; }
        public decimal tasa { get; set; }
        public decimal precio_1 { get; set; }
        public decimal precio_2 { get; set; }
        public int contenido { get; set; }
        public string referencia { get; set; }
        public string nombreEmpq { get; set; }
        public decimal pDetal { get; set; }
        public int contDetal { get; set; }
        public string empDetal { get; set; }
    }
}