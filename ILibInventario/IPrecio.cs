﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ILibInventario
{

    public interface IPrecio
    {

        DtoLib.ResultadoEntidad<DtoLibInventario.Precio.Historico.Resumen> HistoricoPrecio_GetLista(DtoLibInventario.Precio.Historico.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibInventario.Precio.PrecioCosto.Ficha> PrecioCosto_GetFicha(string autoPrd);
        DtoLib.Resultado PrecioProducto_Actualizar(DtoLibInventario.Precio.Editar.Ficha ficha);

    }

}