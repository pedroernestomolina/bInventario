using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.TallaColorSabor.Existencia
{
    public class ExDepTCS
    {
        public string idPrd { get; set; }
        public string idDep { get; set; }
        public int idTCS { get; set; }
        public string NombrePrd { get; set; }
        public string NombreDep { get; set; }
        public string NombreTCS { get; set; }
        public string EstatusTCS { get; set; }
        public decimal Fisica { get; set; }
        public decimal Reservada { get; set; }
        public decimal Disponible { get; set; }
    }
}