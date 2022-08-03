using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Empresa.Grupo.Lista
{
    
    public class Ficha
    {

        public string idGrupo { get; set; }
        public string descripcion { get; set; }


        public Ficha() 
        {
            idGrupo = "";
            descripcion = "";
        }

    }

}