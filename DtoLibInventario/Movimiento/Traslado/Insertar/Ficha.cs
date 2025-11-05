using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Insertar
{
    public class Ficha
    {
        public FichaMov mov { get; set; }
        public List<FichaMovDetalle> detalles { get; set; }
        public List<FichaMovDeposito> prdDeposito { get; set; }
        public List<FichaMovKardex> movKardex { get; set; }
        public Ficha() 
        {
            mov = new FichaMov();
            detalles= new List<FichaMovDetalle>();
            prdDeposito = new List<FichaMovDeposito>();
            movKardex = new List<FichaMovKardex>();
        }
    }
}