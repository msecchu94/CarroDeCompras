using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly IConnectionFactory _connectionFactory;

        public RepositoryUsuario(IConnectionFactory ConnectionFactory)
        {

            this._connectionFactory = ConnectionFactory;
        }

        public UsuarioBE AltaUsuario(UsuarioBE usuarioBE)
        {

            #region Query


            string queryUsuarioInsert = @"INSERT INTO Usuarios 
                                    (Usuario,Nombre,Apellido,Activo,PasswordSalt,Password,IdRol)
                                    VALUES(@Usuario,@Nombre,@Apellido,@Activo,@PasswordSalt,@Password,@IdRol)";

            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                UsuarioBE usuario = null;
                try
                {
                    usuario = Connection.QueryFirstOrDefault<UsuarioBE>(queryUsuarioInsert, param: usuarioBE);
                }
                catch (Exception)
                {

                    throw;
                }
                return usuario;

            }
        }

        public UsuarioBE ObtenerUsuario(UsuarioBE usuarioBE)
        {

            #region Query

            string queryUsuario = @"SELECT 
                                       [Id]
                                       ,[Activo]
                                       ,[Usuario]
                                       ,[Password]
                                       ,[PasswordSalt]
                                       ,[Nombre]
                                       ,[IdRol]
                                       FROM [Usuarios]
                                       WHERE [Usuario]=@Usuario";
            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                   var usuarioResult = Connection.QueryFirstOrDefault<UsuarioBE>(queryUsuario, param: usuarioBE);

                   return usuarioResult;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public UsuarioBE ModificarPassword(UsuarioBE usuarioBE)
        {
            #region Query

            string queryEditPassword = @"UPDATE Usuarios
                                   SET Password=@Password,
                                   PasswordSalt=@PasswordSalt
                                   WHERE [Usuario]=@Usuario";
            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {

                UsuarioBE usuario = null;

                try
                {
                    usuario = Connection.QueryFirstOrDefault<UsuarioBE>(queryEditPassword, param: usuarioBE);
                }
                catch (Exception)
                {
                    throw new Exception("No se pudo obtener Usuario");
                }

                return usuarioBE;
            }
        }
    }
}
