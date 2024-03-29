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

        public DtoLib.ResultadoLista<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetLista()
        {
            return ServiceProv.TasaImpuesto_GetLista();
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetById(string id)
        {
            return ServiceProv.TasaImpuesto_GetById(id);
        }

    }

}
