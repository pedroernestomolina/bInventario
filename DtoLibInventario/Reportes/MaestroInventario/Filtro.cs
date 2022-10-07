using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroInventario
{
    
    public class Filtro
    {

        public string autoDeposito { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoTasa { get; set; }
        public enumerados.EnumAdministradorPorDivisa admDivisa { get; set; }
        public enumerados.EnumPesado pesado { get; set; }


        public Filtro()
        {
            autoDepartamento = "";
            autoDeposito = "";
            autoGrupo = "";
            autoTasa = "";
            admDivisa = enumerados.EnumAdministradorPorDivisa.SnDefinir;
            pesado = enumerados.EnumPesado.SnDefinir;
        }

    }

}