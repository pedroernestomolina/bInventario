using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Cargo.Insertar
{
    
    public class Ficha
    {


        public FichaMov mov { get; set; }
        public List<FichaMovDetalle> movDetalles { get; set; }
        public List<FichaMovDeposito> movDeposito { get; set; }
        public List<FichaMovKardex> movKardex { get; set; }
        public List<FichaPrdCosto> prdCosto { get; set; }
        public List<FichaPrdCostoHistorico> prdCostoHistorico { get; set; }
        public List<FichaPrdPrecio> prdPrecio { get; set; }
        public List<FichaPrdPrecioMargen> prdPrecioMargen { get; set; }
        public List<FichaPrdPrecioHistorico> prdPrecioHistorico { get; set; }


        public Ficha()
        {
            mov = new FichaMov();
            movDetalles = new List<FichaMovDetalle>();
            movDeposito = new List<FichaMovDeposito>();
            movKardex = new List<FichaMovKardex>();
            prdCosto = new List<FichaPrdCosto>();
            prdCostoHistorico = new List<FichaPrdCostoHistorico>();
            prdPrecio = new List<FichaPrdPrecio>();
            prdPrecioMargen = new List<FichaPrdPrecioMargen>();
            prdPrecioHistorico = new List<FichaPrdPrecioHistorico>();
        }

    }

}