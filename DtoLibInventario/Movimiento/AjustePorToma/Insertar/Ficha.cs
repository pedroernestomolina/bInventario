using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.AjustePorToma.Insertar
{
    public class Ficha
    {
        public string idToma { get; set; }
        public FichaMov mov { get; set; }
        public List<FichaMovDetalle> movDetalles { get; set; }
        public List<FichaMovDeposito> movDeposito { get; set; }
        public List<FichaMovKardex> movKardex { get; set; }
        public List<FichaPrdToma> prdToma { get; set; }
        public Ficha() 
        {
            idToma = "";
            mov = new FichaMov();
            movDetalles= new List<FichaMovDetalle>();
            movDeposito = new List<FichaMovDeposito>();
            movKardex = new List<FichaMovKardex>();
            prdToma= new List<FichaPrdToma>();
        }
    }
}