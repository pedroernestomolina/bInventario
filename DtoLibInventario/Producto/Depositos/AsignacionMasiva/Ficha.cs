using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Producto.Depositos.AsignacionMasiva
{
    
    public class Ficha
    {

        public FichaDepositoDestino depositoDestino { get; set; }
        public List<FichaDepartamentos> departamentosNoIncluir { get; set; }


        public Ficha() 
        {
            depositoDestino = new FichaDepositoDestino();
            departamentosNoIncluir = new List<FichaDepartamentos>();
        }

    }

}