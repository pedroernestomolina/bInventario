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

        public DtoLib.ResultadoLista<DtoLibInventario.Empresa.Grupo.Lista.Ficha> 
            EmpresaGrupo_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibInventario.Empresa.Grupo.Lista.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var _sql_1 = @"select 
                                        auto as idGrupo, 
                                        nombre as descripcion 
                                    from empresa_grupo";
                    var _lst = cnn.Database.SqlQuery<DtoLibInventario.Empresa.Grupo.Lista.Ficha>(_sql_1).ToList();
                    result.Lista = _lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            EmpresaGrupo_PrecioManejar_GetById(string idGrupo)
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idGrupo);
                    var _sql_1 = @"select 
                                        eHndPrecio.codigo
                                    from empresa_grupo_ext as gExt
                                    join empresa_hnd_precios as eHndPrecio on eHndPrecio.id = gExt.idEmpresaHndPrecio 
                                    where gExt.auto_empresaGrupo=@id";
                    var _ent = cnn.Database.SqlQuery<string>(_sql_1, p1).FirstOrDefault();
                    if (_ent == null) 
                    {
                        result.Entidad = "";
                    }
                    result.Entidad= _ent.ToString();
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