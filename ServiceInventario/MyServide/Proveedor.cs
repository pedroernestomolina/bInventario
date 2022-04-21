﻿using ServiceInventario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceInventario.MyService
{

    public partial class Service: IService
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Proveedor.Lista.Resumen> Proveedor_GetLista(DtoLibInventario.Proveedor.Lista.Filtro filtro)
        {
            return ServiceProv.Proveedor_GetLista(filtro);
        }

    }

}
