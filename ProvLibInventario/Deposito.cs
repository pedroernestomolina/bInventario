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
        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> 
            Deposito_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen>();
            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var xsql = @"SELECT 
                                    ed.auto, 
                                    ed.codigo, 
                                    ed.nombre, 
                                    edExt.es_activo as estatusActivo, 
                                    edExt.es_predeterminado as estatusPredeterminado
                                FROM empresa_depositos as ed 
                                join empresa_depositos_ext as edExt on ed.auto=edExt.auto_deposito 
                                where edExt.es_activo='1'";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Deposito.Resumen>(xsql).ToList(); ;
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
        public DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha> 
            Deposito_GetFicha(string autoDep)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Deposito.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.empresa_depositos.Find(autoDep);

                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var _autoSuc="";
                    var _codSuc="";
                    var _nomSuc="";
                    var entSuc= cnn.empresa_sucursal.FirstOrDefault(f=>f.codigo==ent.codigo_sucursal);
                    if (entSuc!=null)
                    {
                        _autoSuc=entSuc.auto;
                        _codSuc=entSuc.codigo;
                        _nomSuc=entSuc.nombre;
                    };

                    var nr = new DtoLibInventario.Deposito.Ficha()
                    {
                        auto = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        autoSucursal = _autoSuc,
                        codigoSucursal = _codSuc,
                        nombreSucursal = _nomSuc,
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
        public DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen> 
            Deposito_GetListaBySucursal(string codSuc)
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Deposito.Resumen>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@codSucursal", codSuc.Trim().ToUpper());
                    var xsql = @"SELECT ed.auto, ed.codigo, ed.nombre 
                                FROM empresa_depositos as ed 
                                join empresa_depositos_ext as edExt on ed.auto=edExt.auto_deposito 
                                where upper(trim(ed.codigo_sucursal))=@codSucursal and edExt.es_activo='1'";
                    var lst = cnn.Database.SqlQuery<DtoLibInventario.Deposito.Resumen>(xsql, p1).ToList(); ;
                    result.Lista = lst;

                    //var q = cnn.empresa_depositos.Where(w=>w.codigo_sucursal.Trim().ToUpper()==codSuc.Trim().ToUpper()).ToList();
                    //var list = new List<DtoLibInventario.Deposito.Resumen>();
                    //if (q != null)
                    //{
                    //    if (q.Count() > 0)
                    //    {
                    //        list = q.Select(s =>
                    //        {
                    //            var r = new DtoLibInventario.Deposito.Resumen()
                    //            {
                    //                auto = s.auto,
                    //                codigo = s.codigo,
                    //                nombre = s.nombre,
                    //            };
                    //            return r;
                    //        }).ToList();
                    //    }
                    //}
                    //result.Lista = list;
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