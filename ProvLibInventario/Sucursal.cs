using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvLibInventario
{
    
    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen> Sucursal_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.empresa_sucursal.ToList();

                    var list = new List<DtoLibInventario.Sucursal.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Sucursal.Resumen()
                                {
                                    auto = s.auto,
                                    codigo = s.codigo,
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha> Sucursal_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.empresa_sucursal.Find(auto);

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] SUCURSAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var depCodigo="";
                    var depNombre="";
                    var depAuto="";
                    if (ent.autoDepositoPrincipal.Trim()!="")
                    {
                        var entDeposito= cnn.empresa_depositos.Find(ent.autoDepositoPrincipal);
                        depAuto=entDeposito.auto;
                        depCodigo=entDeposito.codigo;
                        depNombre=entDeposito.nombre;
                    };

                    var grupoAuto ="";
                    var grupoNombre="";
                    if (ent.autoEmpresaGrupo.Trim()!="")
                    {
                        var entGrupoEmpresa= cnn.empresa_grupo.Find(ent.autoEmpresaGrupo);
                        grupoAuto = entGrupoEmpresa.auto;
                        grupoNombre = entGrupoEmpresa.nombre;
                    }

                    var nr = new DtoLibInventario.Sucursal.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        autoDepositoPrincipal = depAuto,
                        autoEmpresaGrupo = grupoAuto,
                        codigoDepositoPrincipal = depCodigo,
                        nombreDepositoPrincipal = depNombre,
                        nombreEmpresaGrupo = grupoNombre,
                    };
                    result.Entidad = nr;
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