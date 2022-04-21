using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Top20
{
    
    public class Filtro
    {

        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public enumerados.EnumModulo Modulo { get; set; }
        public string autoDeposito { get; set; }


        public Filtro()
        {
            autoDeposito = "";
        }

    }

}