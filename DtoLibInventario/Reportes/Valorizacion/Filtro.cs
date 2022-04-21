using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Valorizacion
{
    
    public class Filtro
    {

        public DateTime hasta { get; set; }
        public string idDeposito { get; set; }


        public Filtro()
        {
            hasta = DateTime.Now.Date;
            idDeposito = "";
        }

    }

}