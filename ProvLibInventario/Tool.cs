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

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha>
            Tools_AjusteNivelMinimoMaximo_GetLista(DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var cmd = "SELECT p.auto as autoProducto, p.codigo as codigoProducto, p.nombre as nombreProducto, " +
                        "p.referencia as referenciaProducto, p.estatus_cambio as esSuspendido, " +
                        "case p.estatus_pesado " +
                        "WHEN '0' THEN 'N' " +
                        "WHEN '1' THEN 'S' " +
                        "END as esPesado, " +
                        "dep.fisica as fisica, dep.nivel_minimo as nivelMinimo, dep.nivel_optimo as nivelOptimo, med.decimales " +
                        "FROM `productos_deposito` as dep " +
                        "JOIN productos as p on dep.auto_producto=p.auto " +
                        "JOIN productos_medida as med on p.auto_empaque_compra=med.auto " +
                        "where dep.auto_deposito=@dep and p.estatus='Activo' ";

                    var dep = new MySql.Data.MySqlClient.MySqlParameter("@dep", filtro.autoDeposito);
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDepartamento != "")
                    {
                        cmd += "and p.auto_departamento=@p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = filtro.autoDepartamento;
                    }

                    if (filtro.cadena.Trim() != "")
                    {
                        var xcadena = filtro.cadena.Trim();
                        cmd += " and p.nombre like @p2 ";
                        p2.ParameterName = "@p2";
                        if (xcadena.Substring(0, 1) == "*")
                            if (xcadena.Length > 1)
                                p2.Value = "%" + filtro.cadena.Substring(1) + "%";
                            else
                                p2.Value = "%%";
                        else
                            p2.Value = xcadena + "%";
                    }

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha>(cmd, dep, p1, p2).ToList();
                    result.Lista = list;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado
            Tools_AjusteNivelMinimoMaximo_Ajustar(List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar.Ficha> listaAjuste)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        foreach (var it in listaAjuste)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == it.autoProducto &&
                                f.auto_deposito == it.autoDeposito);
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje = "[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO";
                                return result;
                            }
                            entPrdDep.nivel_minimo = it.nivelMinimo;
                            entPrdDep.nivel_optimo = it.nivelOptimo;
                            cnn.SaveChanges();
                        };

                        ts.Complete();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }

        public DtoLib.ResultadoLista<DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Ficha>
            Tools_CambioMasivoPrecio_GetData(DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var id = filtro.codigoPrecioOrigen;
                    if (filtro.codigoPrecioOrigen == "5") { id = "pto"; }
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"SELECT 
                                    auto as autoPrd, 
                                    auto_precio_" + id + @" as autoPrecioEmp_1, 
                                    pExt.auto_precio_may_" + id + @" as autoPrecioEmp_2, 
                                    pExt.auto_precio_dsp_" + id + @" as autoPrecioEmp_3,
                                    precio_" + id + @" as pNetoEmp_1, 
                                    pExt.precio_may_" + id + @" as pNetoEmp_2, 
                                    pExt.precio_dsp_" + id + @" as pNetoEmp_3,
                                    contenido_" + id + @" as contEmp_1, 
                                    pExt.contenido_may_" + id + @" as contEmp_2, 
                                    pExt.cont_dsp_" + id + @" as contEmp_3,
                                    utilidad_" + id + @" as utEmp_1, 
                                    pExt.utilidad_may_" + id + @" as utEmp_2, 
                                    pExt.utilidad_dsp_" + id + @" as utEmp_3,
                                    pdf_" + id + @" as pFullDivEmp_1, 
                                    pExt.pdmf_" + id + @" as pFullDivEmp_2,
                                    pExt.pdivisafull_dsp_" + id + @" as pFullDivEmp_3 ";
                    var _sql_2 = @" FROM `productos` as p
                                    join productos_ext as pExt on pExt.auto_producto=p.auto ";
                    var _sql_3 = @" where 1=1 and 
                                    p.estatus='Activo' and 
                                    p.categoria!='Bien de Servicio' ";
                    if (filtro.idDepartamento != "")
                    {
                        p1.ParameterName = "@idDepartamento";
                        p1.Value = filtro.idDepartamento;
                        _sql_3 += " and p.auto_departamento=@idDepartamento ";
                    }
                    if (filtro.idGrupo != "")
                    {
                        p2.ParameterName = "@idGrupo";
                        p2.Value = filtro.idGrupo;
                        _sql_3 += " and p.auto_grupo=@idGrupo";
                    }

                    var sql = _sql_1 + _sql_2 + _sql_3;
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Tool.CambioMasivoPrecio.CapturarData.Ficha>(sql, p1, p2).ToList();
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado
            Tools_CambioMasivoPrecio_ActualizarData(DtoLibInventario.Tool.CambioMasivoPrecio.ActualizarData.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {

                        var id = ficha.codPrecioDestino;
                        if (id == "5") { id = "pto"; }
                        var p0 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter();

                        var _sql_1 = @"UPDATE 
                                        productos
                                        set
                                            auto_precio_" + id + @"=@autoPrecioEmp_1, 
                                            precio_" + id + @"=@pNetoEmp_1, 
                                            contenido_" + id + @"=@contEmp_1, 
                                            utilidad_" + id + @"=@utEmp_1, 
                                            pdf_" + id + @"=@pFullDivEmp_1 
                                        where auto=@autoPrd";

                        var _sql_2_1 = @"UPDATE 
                                        productos_ext 
                                        set
                                            auto_precio_may_" + id + @"=@autoPrecioEmp_2, 
                                            auto_precio_dsp_" + id + @"=@autoPrecioEmp_3,
                                            precio_may_" + id + @"=@pNetoEmp_2, 
                                            precio_dsp_" + id + @"=@pNetoEmp_3,
                                            cont_dsp_" + id + @"=@contEmp_3,
                                            utilidad_may_" + id + @"=@utEmp_2, 
                                            utilidad_dsp_" + id + @"=@utEmp_3,
                                            pdmf_" + id + @"=@pFullDivEmp_2,
                                            pdivisafull_dsp_" + id + @"=@pFullDivEmp_3 ";
                        if (ficha.codPrecioDestino == "4")
                            _sql_2_1 += @", cont_may_" + id + @"=@contEmp_2 ";
                        else
                            _sql_2_1 += @", contenido_may_" + id + @"=@contEmp_2 ";
                        var _sql_2_2= @" where auto_producto=@autoPrd ";

                        foreach (var rg in ficha.data)
                        {
                            p0.ParameterName = "@autoPrd";
                            p0.Value = rg.autoPrd;
                            //
                            p1.ParameterName = "@autoPrecioEmp_1";
                            p1.Value = rg.autoPrecioEmp_1;
                            p2.ParameterName = "@autoPrecioEmp_2";
                            p2.Value = rg.autoPrecioEmp_2;
                            p3.ParameterName = "@autoPrecioEmp_3";
                            p3.Value = rg.autoPrecioEmp_3;
                            //
                            p4.ParameterName = "@pNetoEmp_1";
                            p4.Value = rg.pNetoEmp_1;
                            p5.ParameterName = "@pNetoEmp_2";
                            p5.Value = rg.pNetoEmp_2;
                            p6.ParameterName = "@pNetoEmp_3";
                            p6.Value = rg.pNetoEmp_3;
                            //
                            p7.ParameterName = "@contEmp_1";
                            p7.Value = rg.contEmp_1;
                            p8.ParameterName = "@contEmp_2";
                            p8.Value = rg.contEmp_2;
                            p9.ParameterName = "@contEmp_3";
                            p9.Value = rg.contEmp_3;
                            //
                            p10.ParameterName = "@utEmp_1";
                            p10.Value = rg.utEmp_1;
                            p11.ParameterName = "@utEmp_2";
                            p11.Value = rg.utEmp_2;
                            p12.ParameterName = "@utEmp_3";
                            p12.Value = rg.utEmp_3;
                            //
                            p13.ParameterName = "@pFullDivEmp_1";
                            p13.Value = rg.pFullDivEmp_1;
                            p14.ParameterName = "@pFullDivEmp_2";
                            p14.Value = rg.pFullDivEmp_2;
                            p15.ParameterName = "@pFullDivEmp_3";
                            p15.Value = rg.pFullDivEmp_3;
                            //
                            var r1 = cnn.Database.ExecuteSqlCommand(_sql_1, p0, p1, p4, p7, p10, p13);
                            cnn.SaveChanges();

                            var _sql_2 = _sql_2_1 + _sql_2_2;
                            var r2 = cnn.Database.ExecuteSqlCommand(_sql_2, p0, p2, p3, p5, p6, p8, p9, p11, p12, p14, p15);
                            cnn.SaveChanges();

                            //var r2 = cnn.Database.ExecuteSqlCommand(_sql_2, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
                            //cnn.SaveChanges();

                        }
                        ts.Complete();
                    };
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
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