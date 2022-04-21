using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.AjusteInvCero.Insertar
{
    
    public class Ficha
    {

        public FichaMov mov { get; set; }
        public List<FichaMovDeposito> movDeposito { get; set; }
        public List<FichaMovDetalle> movDetalles { get; set; }
        public List<FichaMovKardex> movKardex { get; set; }


        public Ficha() 
        {
            mov = new FichaMov();
            movDeposito = new List<FichaMovDeposito>();
            movDetalles= new List<FichaMovDetalle>();
            movKardex = new List<FichaMovKardex>();
        }

    }

}