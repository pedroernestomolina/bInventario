using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.Existencia
{

    public class Filtro
    {

        public string autoDeposito { get; set; }
        public string autoDepartamento { get; set; }
        public Enumerados.enumFiltrarPor filtrarPor { get; set; }


        public Filtro()
        {
            autoDepartamento = "";
            autoDeposito = "";
            filtrarPor = Enumerados.enumFiltrarPor.SinDefinir;
        }

    }

}