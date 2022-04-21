using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Configuracion.DepositoPredeterminado
{
    
    public class Item
    {

        public string AutoDeposito { get; set; }
        public string Estatus { get; set; }

        public Item() 
        {
            AutoDeposito = "";
            Estatus = "";
        }

    }

}