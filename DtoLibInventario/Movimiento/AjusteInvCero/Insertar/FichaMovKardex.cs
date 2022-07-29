using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.AjusteInvCero.Insertar
{
    
    public class FichaMovKardex: Movimiento.Insertar.BaseFichaMovKardex 
    {


        public FichaMovKardex() 
        {
            autoProducto = "";
            total = 0m;
            autoDeposito = "";
            autoConcepto = "";
            modulo = "";
            entidad = "";
            signoMov = 1;
            cantidad = 0m;
            cantidadBono = 0m;
            cantidadUnd = 0m;
            costoUnd = 0m;
            estatusAnulado = "";
            nota = "";
            precioUnd = 0m;
            codigoMov = "";
            siglasMov = "";
            codigoSucursal = "";
            codigoConcepto = "";
            nombreConcepto = "";
            codigoDeposito = "";
            nombreDeposito = "";
            factorCambio = 0m;
        }

    }

}