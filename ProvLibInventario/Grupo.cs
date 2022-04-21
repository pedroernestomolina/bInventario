using LibEntityInventario;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvLibInventario
{

    public partial class Provider : ILibInventario.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> Grupo_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var q = cnn.productos_grupo.ToList();

                    var list = new List<DtoLibInventario.Grupo.Resumen>();
                    if (q != null)
                    {
                        if (q.Count() > 0)
                        {
                            list = q.Select(s =>
                            {
                                var r = new DtoLibInventario.Grupo.Resumen()
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Grupo.Ficha> Grupo_GetFicha(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Grupo.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.productos_grupo.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] GRUPO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    var nr = new DtoLibInventario.Grupo.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
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
        public DtoLib.ResultadoAuto Grupo_Agregar(DtoLibInventario.Grupo.Agregar ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var sql = "update sistema_contadores set a_productos_grupo=a_productos_grupo+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aGrupo = cnn.Database.SqlQuery<int>("select a_productos_grupo from sistema_contadores").FirstOrDefault();
                        var autoGrupo = aGrupo.ToString().Trim().PadLeft(10, '0');

                        var ent = new  productos_grupo()
                        {
                            auto = autoGrupo,
                            nombre = ficha.nombre,
                            codigo = ficha.codigo,
                            estatus_catalogo="0",
                        };
                        cnn.productos_grupo.Add(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                        result.Auto = autoGrupo;
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var dbUpdateEx = ex as DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1451)
                        {
                            result.Mensaje = "REGISTRO CONTIENE DATA RELACIONADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (exx.Number == 1062)
                        {
                            result.Mensaje = exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                    }
                }
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado Grupo_Editar(DtoLibInventario.Grupo.Editar ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.productos_grupo.Find(ficha.auto);
                        if (ent == null)
                        {
                            result.Mensaje = "[ ID ] ENTIDAD GRUPO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        ent.codigo = ficha.codigo;
                        ent.nombre = ficha.nombre;
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var dbUpdateEx = ex as DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1451)
                        {
                            result.Mensaje = "REGISTRO CONTIENE DATA RELACIONADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (exx.Number == 1062)
                        {
                            result.Mensaje = exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                    }
                }
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen> Grupo_GetListaByDepartamento(string id)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Grupo.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = " SELECT pg.auto, pg.nombre, pg.codigo ";
                    var sql_2 = " from productos_grupo as pg "+
                        " join productos as p on pg.auto=p.auto_grupo "+
                        " join empresa_departamentos as ed on ed.auto=p.auto_departamento ";
                    var sql_3 = " where ed.auto= @p1 ";
                    var sql_4 = " group by pg.auto, pg.nombre, pg.codigo ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "p1";
                    p1.Value = id;

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Grupo.Resumen>(sql,p1).ToList();
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
        public DtoLib.Resultado Grupo_Eliminar(string auto)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.productos_grupo.Find(auto);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] GRUPO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    };
                    cnn.productos_grupo.Remove(ent);
                    cnn.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                var dbUpdateEx = ex as DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1451)
                        {
                            result.Mensaje = "REGISTRO CONTIENE DATA RELACIONADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                    }
                }
                result.Mensaje = ex.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
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