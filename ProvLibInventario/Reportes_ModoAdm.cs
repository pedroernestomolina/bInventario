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
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.ModoAdm.Ficha> 
            Reportes_ModAdm_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.ModoAdm.Ficha>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = @"select 
                                    p.codigo as prdCodigo, 
                                    p.nombre as prdNombre, 
                                    p.estatus_divisa as estatusDivisa, 
                                    x1.neto_monedaLocal as netoMonLocal,
                                    x1.full_divisa as fullDivisa,
                                    x2.contenido_empaque as empCont,
                                    x3.nombre as empDesc,
                                    empTasa.tasa as tasaIva,
                                    empDepart.nombre as departamento,
                                    grupo.nombre as grupo,
                                    x2.tipo_empaque as tipoEmpVta";
                    var sql_2= @" from productos as p
                                join empresa_departamentos as empDepart on empDepart.auto=p.auto_departamento
                                join productos_grupo as grupo on grupo.auto=p.auto_grupo
                                join empresa_tasas as empTasa on empTasa.auto=p.auto_tasa
                                join productos_ext_hnd_precioventa as x1 on x1.auto_producto=p.auto
                                join productos_ext_hnd_empventa as x2 on x2.id=x1.id_prd_hnd_empventa
                                join productos_medida as x3 on x3.auto=x2.auto_empaque ";
                    var sql_3 = @" where x1.neto_monedaLocal>0 ";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    if (filtro.autoDepositoPrincipal != "")
                    {
                        sql_2 += @" join productos_deposito as pDeposito on pDeposito.auto_producto=p.auto 
                                        and pDeposito.auto_deposito=@idDeposito ";
                        p7.ParameterName = "@idDeposito";
                        p7.Value = filtro.autoDepositoPrincipal;
                    }
                    if (filtro.autoDepartamento != "")
                    {
                        sql_3 += " and p.auto_departamento=@autoDepartamento ";
                        p1.ParameterName = "@autoDepartamento";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_3 += " and p.auto_grupo=@autoGrupo ";
                        p2.ParameterName = "@autoGrupo";
                        p2.Value = filtro.autoGrupo;
                    }
                    if (filtro.autoMarca != "")
                    {
                        sql_3 += " and p.auto_marca=@autoMarca ";
                        p3.ParameterName = "@autoMarca";
                        p3.Value = filtro.autoMarca;
                    }
                    if (filtro.autoTasa != "")
                    {
                        sql_3 += " and p.auto_tasa=@autoTasa ";
                        p4.ParameterName = "@autoTasa";
                        p4.Value = filtro.autoTasa;
                    }
                    if (filtro.admDivisa != DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.SnDefinir)
                    {
                        var f = "1";
                        if (filtro.admDivisa == DtoLibInventario.Reportes.enumerados.EnumAdministradorPorDivisa.No)
                            f = "0";
                        sql_3 += " and p.estatus_divisa=@estatusDivisa ";
                        p5.ParameterName = "@estatusDivisa";
                        p5.Value = f;
                    }
                    if (filtro.pesado != DtoLibInventario.Reportes.enumerados.EnumPesado.SnDefinir)
                    {
                        var f = "1";
                        if (filtro.pesado == DtoLibInventario.Reportes.enumerados.EnumPesado.No) { f = "0"; }
                        sql_3 += " and p.estatus_pesado=@estatusPesado ";
                        p6.ParameterName = "@estatusPesado";
                        p6.Value = f;
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroPrecio.ModoAdm.Ficha>(sql, p1, p2, p3, 
                                                                                                        p4, p5, p6, 
                                                                                                        p7, p8).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }
    }
}