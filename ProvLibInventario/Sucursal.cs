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

        public DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen> 
            Sucursal_GetLista(DtoLibInventario.Sucursal.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Sucursal.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"select auto, codigo, nombre 
                                        from empresa_sucursal ";
                    var _sql_2 = " where 1=1 ";
                    if (filtro.idEmpresaGrupo != "") 
                    {
                        _sql_2 += " and autoEmpresaGrupo=@IdEmpresaGrupo ";
                        p1.ParameterName = "@IdEmpresaGrupo";
                        p1.Value = filtro.idEmpresaGrupo;
                    }
                    var sql= _sql_1+_sql_2;
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.Sucursal.Resumen>(sql, p1).ToList();
                    result.Lista = _lst;
                    //var q = cnn.empresa_sucursal.ToList();
                    //var list = new List<DtoLibInventario.Sucursal.Resumen>();
                    //if (q != null)
                    //{
                    //    if (q.Count() > 0)
                    //    {
                    //        list = q.Select(s =>
                    //        {
                    //            var r = new DtoLibInventario.Sucursal.Resumen()
                    //            {
                    //                auto = s.auto,
                    //                codigo = s.codigo,
                    //                nombre = s.nombre,
                    //            };
                    //            return r;
                    //        }).ToList();
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Sucursal.Ficha> 
            Sucursal_GetFicha(string auto)
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