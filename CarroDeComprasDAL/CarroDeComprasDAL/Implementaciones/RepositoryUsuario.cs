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

        #region Querys

        private const string queryInsert = @"INSERT INTO Usuarios (Usuario,Nombre,Apellido,Activo,PasswordSalt,Password,IdRol)
                                           VALUES(@Usuario,@Nombre,@Apellido,@Activo,@PasswordSalt,@Password,@IdRol)";

        private const string queryObtenerUsuario = @"SELECT [Id],[Activo],[Usuario],[Password],[PasswordSalt],[Nombre],[IdRol]
                                                   FROM [Usuarios] WHERE [Usuario]=@Usuario";

        private const string queryEditPassword = @"UPDATE Usuarios SET Password=@Password,PasswordSalt=@PasswordSalt WHERE [Usuario]=@Usuario";

        #endregion

        public RepositoryUsuario(IConnectionFactory ConnectionFactory)
        {

            this._connectionFactory = ConnectionFactory;
        }

        public UsuarioBE AltaUsuario(UsuarioBE usuarioBE)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                UsuarioBE usuario = null;
                usuario = Connection.QueryFirstOrDefault<UsuarioBE>(queryInsert, param: usuarioBE);

                return usuario;
            }
        }

        public UsuarioBE ObtenerUsuario(UsuarioBE usuarioBE)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var usuarioResult = Connection.QueryFirstOrDefault<UsuarioBE>(queryObtenerUsuario, param: usuarioBE);

                return usuarioResult;
            }
        }

        public UsuarioBE ModificarPassword(UsuarioBE usuarioBE)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                UsuarioBE usuario = null;
                usuario = Connection.QueryFirstOrDefault<UsuarioBE>(queryEditPassword, param: usuarioBE);

                return usuarioBE;
            }
        }
    }
}
