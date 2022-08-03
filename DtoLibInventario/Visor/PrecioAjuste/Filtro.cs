using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Visor.PrecioAjuste
{
    
    public class Filtro: BaseFiltro
    {


        public string idEmpresaGrrupo { get; set; }

        
        public Filtro()
        {
            autoDepart = "";
            autoGrupo = "";
            idEmpresaGrrupo = "";
        }

    }

}