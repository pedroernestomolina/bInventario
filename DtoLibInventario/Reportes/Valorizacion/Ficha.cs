using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Valorizacion
{

    public class Ficha
    {

        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal costoUnd { get; set; }
        public int contEmpComp { get; set; }
        public string departamento { get; set; }
        public string grupo { get; set; }
        public decimal cntUnd { get; set; }
        public decimal? costoHist { get; set; }
        public decimal divisa { get; set; }


        public Ficha()
        {
            auto = "";
            codigo = "";
            nombre = "";
            departamento = "";
            grupo = "";
            costoUnd = 0.0m;
            costoHist = null;
            contEmpComp = 0;
            cntUnd = 0.0m;
            divisa = 0.0m;
        }

    }

}