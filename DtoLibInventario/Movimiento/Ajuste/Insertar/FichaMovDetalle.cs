using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Ajuste.Insertar
{
    
    public class FichaMovDetalle: Movimiento.Insertar.BaseFichaMovDetalle
    {


        public FichaMovDetalle() 
        {
            autoProducto = "";
            codigoProducto = "";
            nombreProducto = "";
            cantidad=0m;
            cantidadBono = 0m;
            cantidadUnd = 0m;
            categoria = "";
            tipo = "";
            estatusAnulado = "";
            contEmpaque = 0;
            empaque = "";
            decimales = "";
            costoUnd = 0m;
            total = 0m;
            costoCompra = 0m;
            estatusUnidad = "";
            signo = 1;
            autoDepartamento = "";
            autoGrupo = "";
        }

    }

}