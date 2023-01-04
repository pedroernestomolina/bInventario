using EntitySqlServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvSqlServer
{
    public partial class Provider : ILibInventario.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.Ficha> 
            Reportes_MaestroPrecio(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            var rt=new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.Ficha>();
            rt.Result = DtoLib.Enumerados.EnumResult.isError;
            rt.Mensaje = "NO IMPLEMENTAR ESTE METODO";
            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.FichaFox> 
            Reportes_MaestroPrecio_FoxSystem(DtoLibInventario.Reportes.MaestroPrecio.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibInventario.Reportes.MaestroPrecio.FichaFox>();
            try
            {
                using (var cnn = new FoxInvEntities(_cnInv.ConnectionString))
                {
                    var sql_1 = @"select 
                                    p.auto as autoPrd,
                                    p.codigo, 
                                    p.NOMBRE, 
                                    p.tasa, 
                                    p.precio_pto_venta as pDetal,
                                    p.contenido_empaque_venta as contDetal,
                                    pDepart.nombre as Depart,
	                                pGrupo.nombre as Grupo,
                                    emp.precio_1,
	                                emp.precio_2,
	                                emp.contenido,
	                                emp.referencia,
	                                pMedida.nombre as nombreEmpq,
                                    pMedidaDetal.nombre as empDetal
                                from productos_empaque  as emp
                                join productos as p on p.auto=emp.auto_producto
                                join productos_departamento as pDepart on pDepart.auto=p.AUTO_DEPARTAMENTO
                                join productos_grupo as pGrupo on pGrupo.auto=p.AUTO_GRUPO
                                join productos_medida as pMedida on pMedida.auto=emp.auto_medida 
                                join productos_medida as pMedidaDetal on pMedidaDetal.auto=p.auto_medida_venta ";
                    var sql_2 = @" where p.estatus='Activo' 
                                    and (emp.precio_1>0 or emp.precio_2>0 or p.precio_pto_venta>0)";
                    var p1 = new SqlParameter("@autoDepartamento", "");
                    var p2 = new SqlParameter("@autoGrupo", "");
                    var p3 = new SqlParameter("@autoMarca", "");
                    var p4 = new SqlParameter("@estatusPesado", "");
                    if (filtro.autoDepartamento != "")
                    {
                        sql_2 += " and p.auto_departamento=@autoDepartamento ";
                        p1.Value = filtro.autoDepartamento;
                    }
                    if (filtro.autoGrupo != "")
                    {
                        sql_2 += " and p.auto_grupo=@autoGrupo ";
                        p2.Value = filtro.autoGrupo;
                    }
                    if (filtro.autoMarca != "")
                    {
                        sql_2 += " and p.auto_marca=@autoMarca ";
                        p3.Value = filtro.autoMarca;
                    }
                    if (filtro.pesado != DtoLibInventario.Reportes.enumerados.EnumPesado.SnDefinir)
                    {
                        var f = "1";
                        if (filtro.pesado == DtoLibInventario.Reportes.enumerados.EnumPesado.No) { f = "0"; }
                        sql_2 += " and p.estatus_balanza=@estatusPesado ";
                        p4.ParameterName = "@estatusPesado";
                        p4.Value = f;
                    }
                    var sql = sql_1 + sql_2;
                    var list = cnn.Database.SqlQuery<DtoLibInventario.Reportes.MaestroPrecio.FichaFox>(sql, p1, p2, p3, p4).ToList();
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