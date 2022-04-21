using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Configuracion.MetodoCalculoUtilidad.Editar
{
    
    public class Ficha
    {
        
        public string Metodo { get; set; }
        public List<FichaPrecio> Precio{get;set;}


        public Ficha()
        {
            Metodo = "";
            Precio = new List<FichaPrecio>();
        }

    }

}