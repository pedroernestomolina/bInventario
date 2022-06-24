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


        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMaximo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL17");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
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
            Permiso_PedirClaveAcceso_NivelMedio()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL18");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
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
            Permiso_PedirClaveAcceso_NivelMinimo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL19");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearProducto(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0301010000'", p1).FirstOrDefault();
                    if (permiso== null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarProducto(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0301020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarPrecios(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0301040000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarCostos(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0301050000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AsignarDepositos(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarDatosDelDeposito(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ActualizarEstatusDelProducto(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CambiarImagenDelProducto(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330040000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Departamento(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0303000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearDepartamento(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0303010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarDepartamento(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0303020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarDepartamento(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0303030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Grupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0304000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0304010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0304020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarGrupo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0304030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_Marca(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0305000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearMarca(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0305010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarMarca(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0305020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarMarca(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0305030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_UnidadEmpaque(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0306000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearUnidadEmpaque(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0306010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarUnidadEmpaque(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0306020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarUnidadEmpaque(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0306030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ConceptoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0307000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_CrearConceptoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0307010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ModificarConceptoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0307020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_EliminarConcepto(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0307030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_ToolInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0310000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoCargoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoDescargoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTrasladoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTrasladoPorDevolucionInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308050000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoAjusteInventarioCero(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308060000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoAjusteInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308040000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdministradorMovimientoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0309000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdmAnularMovimientoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0309010000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdmVisualizarMovimientoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0309020000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_AdmReporteMovimientoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0309030000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_DefinirNivelMinimoMaximoInventario(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330050000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> Permiso_MovimientoTrasladoEntreSucursales_PorExistenciaDebajoDelMinimo(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330060000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }


        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Reportes(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0399000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Visor(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330070000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Estadistica(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330080000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_Configuracion_Sistema(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1202000000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_MovimientoTraslado_Procesar(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0308070000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha> 
            Permiso_AsignacionMasivaProductosDeposito(string autoGrupoUsuario)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibInventario.Permiso.Ficha>();

            try
            {
                using (var cnn = new invEntities(_cnInv.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoGrupoUsuario;
                    var permiso = cnn.Database.SqlQuery<DtoLibInventario.Permiso.Ficha>("select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0330090000'", p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
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