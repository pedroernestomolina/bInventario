using LibEntityInventario;
using System;
using System.Collections.Generic;
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
                    var cmd = "SELECT p.auto as autoProducto, p.codigo as codigoProducto, p.nombre as nombreProducto, "+
                        "p.referencia as referenciaProducto, p.estatus_cambio as esSuspendido, "+
                        "case p.estatus_pesado "+
                        "WHEN '0' THEN 'N' "+
                        "WHEN '1' THEN 'S' "+
                        "END as esPesado, "+
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
                        cmd+= "and p.auto_departamento=@p1 ";
                        p1.ParameterName="@p1";
                        p1.Value = filtro.autoDepartamento;
                    }

                    if (filtro.cadena.Trim() != "")
                    {
                        var xcadena = filtro.cadena.Trim();
                        cmd += " and p.nombre like @p2 ";
                        p2.ParameterName = "@p2";
                        if (xcadena.Substring(0,1)=="*")
                            if (xcadena.Length>1)
                                p2.Value = "%"+filtro.cadena.Substring(1) + "%";
                            else
                                p2.Value = "%%";
                        else
                            p2.Value = xcadena+"%";
                    }

                    var list = cnn.Database.SqlQuery<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Capturar.Ficha>(cmd, dep, p1, p2).ToList();
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

        public DtoLib.Resultado Tools_AjusteNivelMinimoMaximo_Ajustar(List<DtoLibInventario.Tool.AjusteNivelMinimoMaximo.Ajustar.Ficha> listaAjuste)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        foreach (var it in  listaAjuste)
                        {
                            var entPrdDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == it.autoProducto && 
                                f.auto_deposito == it.autoDeposito );
                            if (entPrdDep == null)
                            {
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                result.Mensaje ="[ ID ] PRODUCTO / DEPOSITO NO ENCONTRADO";
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
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
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