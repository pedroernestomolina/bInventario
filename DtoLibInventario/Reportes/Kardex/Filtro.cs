using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.Kardex
{
    
    public class Filtro
    {

        public string autoProducto { get; set; }
        public string autoDeposito { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }


        public Filtro()
        {
            autoProducto = "";
            autoDeposito = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
        }

    }

}
