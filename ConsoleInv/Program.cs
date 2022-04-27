﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleInv
{


    class Program
    {

        static void Main(string[] args)
        {
            ILibInventario.IProvider invPrv = new ProvLibInventario.Provider("localhost", "pita");
//            var r01 = invPrv.Producto_GetFicha("0000000450");

            //var filtro = new DtoLibInventario.MovPend.Lista.Filtro();
            //var r01 = invPrv.MovPend_GetLista(filtro);
            //var r01 = invPrv.MovPend_Anular(1);

            var filtro = new DtoLibInventario.Producto.Filtro();
            filtro.activarBusquedaPorTrasalado = true;
            filtro.autoDepOrigen = "0000000001";
            filtro.autoDepDestino = "0000000008";
            filtro.estatus = DtoLibInventario.Producto.Enumerados.EnumEstatus.Activo;
            var r01 = invPrv.Producto_GetLista(filtro);
        }

    }

}