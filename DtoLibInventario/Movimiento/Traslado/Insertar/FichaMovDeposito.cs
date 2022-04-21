using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Insertar
{
    
    public class FichaMovDeposito: Movimiento.Insertar.BaseFichaMovDeposito
    {

        public string autoDepositoDestino { get; set; }
        public string depositoDestino { get; set; }


        public FichaMovDeposito() 
        {
            autoProducto = "";
            autoDeposito = "";
            nombreProducto = "";
            nombreDeposito = "";
            cantidadUnd = 0m;
            autoDepositoDestino = "";
            depositoDestino = "";
        }


    }

}