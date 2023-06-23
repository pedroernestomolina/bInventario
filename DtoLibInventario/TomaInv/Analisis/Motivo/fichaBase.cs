using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TomaInv.Analisis.Motivo
{
    abstract public class fichaBase
    {
        public string idToma { get; set; }
        public string idPrd { get; set; }
    }
}