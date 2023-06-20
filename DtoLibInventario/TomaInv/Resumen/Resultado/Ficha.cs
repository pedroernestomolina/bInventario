using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.Resumen.Resultado
{
    public class Ficha
    {
        public string autoPrd { get; set; }
        public string autoDepart { get; set; }
        public string autoGrupo { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string catPrd { get; set; }
        public int contEmp { get; set; }
        public decimal costo { get; set; }
        public string estatusDivisa { get; set; }
        public decimal costoDivisa { get; set; }
        public string autoTasa { get; set; }
        public string descTasa { get; set; }
        public decimal valorTasa { get; set; }
        public decimal cantidadAjustar { get; set; }
        public int signo { get; set; }
        public string descTipoAjuste { get; set; }
        public decimal conteo { get; set; }
    }
}