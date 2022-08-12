using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData
{
    
    public class Filtro
    {

        public string codigoPrecioOrigen { get; set; }
        public string idDepartamento { get; set; }
        public string idGrupo { get; set; }


        public Filtro()
        {
            codigoPrecioOrigen = "";
            idDepartamento = "";
            idGrupo = "";
        }

    }

}