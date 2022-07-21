using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.ResumenCostoInv
{
    
    public class Filtro
    {

        public string autoDeposito { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            autoDepartamento = "";
            autoDeposito = "";
            autoGrupo = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
        }

    }

}