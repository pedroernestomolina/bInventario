using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibInventario.Movimiento.Traslado.Insertar
{
    public class FichaMov: Movimiento.Insertar.BaseFichaMov
    {
        public FichaMov()
        {
            autoConcepto = "";
            autoDepositoDestino = "";
            autoDepositoOrigen = "";
            autoRemision = "";
            autoUsuario = "";
            nota = "";
            estatusAnulado = "";
            usuario = "";
            codUsuario = "";
            estacion = "";
            codConcepto = "";
            desConcepto = "";
            codDepositoOrigen = "";
            desDepositoOrigen = "";
            codDepositoDestino = "";
            desDepositoDestino = "";
            tipo = "";
            renglones = 0;
            documentoNombre = "";
            autorizado = "";
            total = 0m;
            situacion = "";
            codigoSucursal="";
            cierreFtp="";
            estatusCierreContable="";
            factorCambio=0m;
            montoDivisa=0m;
        }
    }
}