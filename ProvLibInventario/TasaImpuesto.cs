﻿using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.TasaImpuesto.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.empresa_tasas.ToList();

                    var list = new List<DtoLibInventario.TasaImpuesto.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.TasaImpuesto.Resumen()
                                {
                                    auto = s.auto,
                                    tasa= s.tasa,
                                    nombre = s.nombre,
                                };
                                return r;
                            }).ToList();
                        }
                    }
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.TasaImpuesto.Resumen> 
            TasaImpuesto_GetById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.TasaImpuesto.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.empresa_tasas.Find(id);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] TASA IMPUESTO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = new DtoLibInventario.TasaImpuesto.Resumen()
                    {
                        auto = ent.auto,
                        tasa = ent.tasa,
                        nombre = ent.nombre,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}