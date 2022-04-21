using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Configuracion.DepositoPredeterminado
{
    
    public class Ficha
    {

        public List<Item> ListaPreDet { get; set; }


        public Ficha() 
        {
            ListaPreDet= new List<Item>();
        }

    }

}