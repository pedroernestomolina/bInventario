using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Capturar.ProductoPorDebajoNivelMinimo
{
    
    public class Filtro
    {

        public string autoDepositoOrigen { get; set; }
        public string autoDepositoVerificarNivel { get; set; }
        public string autoDepartamento { get; set; }


        public Filtro() 
        {
            autoDepartamento = "";
            autoDepositoOrigen = "";
            autoDepositoVerificarNivel = "";
        }

    }

}