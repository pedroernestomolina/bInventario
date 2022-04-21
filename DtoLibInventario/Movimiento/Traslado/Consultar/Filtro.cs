using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Consultar
{

    public class Filtro
    {

        public string autoDeposito { get; set; }
        public string autoDepartamento { get; set; }


        public Filtro()
        {
            autoDeposito = "";
            autoDepartamento = "";
        }

    }

}