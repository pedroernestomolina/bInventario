using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Reportes.MaestroPrecio
{
    
    public class Filtro
    {

        public string autoMarca { get; set; }
        public string autoGrupo { get; set; }
        public string autoDepartamento { get; set; }
        public string autoTasa { get; set; }
        public enumerados.EnumAdministradorPorDivisa admDivisa { get; set; }
        public enumerados.EnumOrigen origen { get; set; }
        public enumerados.EnumCategoria categoria { get; set; }
        public enumerados.EnumPesado pesado { get; set; }
        public enumerados.EnumPrecio precio { get; set; }


        public Filtro()
        {
            admDivisa = enumerados.EnumAdministradorPorDivisa.SnDefinir;
            origen = enumerados.EnumOrigen.SnDefinir;
            categoria = enumerados.EnumCategoria.SnDefinir;
            precio = enumerados.EnumPrecio.SnDefinir;
            pesado = enumerados.EnumPesado.SnDefinir;
            autoGrupo = "";
            autoMarca = "";
            autoDepartamento = "";
            autoTasa = "";
        }

    }

}