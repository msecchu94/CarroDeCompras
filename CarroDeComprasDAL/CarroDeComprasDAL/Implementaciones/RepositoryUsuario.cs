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
        public SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);

        private string queryUsuario = @"SELECT 
                                       [Id]
                                       ,[Activo]
                                       ,[Usuario]
                                       ,[Password]
                                       ,[PasswordSalt]
                                       ,[Nombre]
                                       ,[IdRol]
                                       FROM [Usuarios]
                                       WHERE [Usuario]=@Usuario";

        string queryUsuarioInsert = @"INSERT INTO Usuarios 
                                    (Usuario,Nombre,Apellido,Activo,PasswordSalt,Password,IdRol)
                                    VALUES(@Usuario,@Nombre,@Apellido,@Activo,@PasswordSalt,@Password,@IdRol)";

        string queryEditPassword = @"UPDATE Usuarios
                                   SET Password=@Password,
                                   PasswordSalt=@PasswordSalt
                                   WHERE [Usuario]=@Usuario";

        public UsuarioBE AltaUsuario(UsuarioBE usuarioBE)
        {
            UsuarioBE usuario = null;
            try
            {
                usuario = ConnectionString.QueryFirstOrDefault<UsuarioBE>(queryUsuarioInsert, param: usuarioBE);
            }
            catch (Exception)
            {

                throw;
            }
            return usuario;


        }

        public UsuarioBE ObtenerUsuario(UsuarioBE usuarioBE)
        {
            UsuarioBE usuario = ConnectionString.QueryFirstOrDefault<UsuarioBE>(queryUsuario, param: usuarioBE);

            return usuario;
        }

        public UsuarioBE ModificarPassword(UsuarioBE usuarioBE)
        {

            UsuarioBE usuario = null;

            try
            {
                usuario = ConnectionString.QueryFirstOrDefault<UsuarioBE>(queryEditPassword, param: usuarioBE);
            }
            catch (Exception)
            {

                throw new Exception("No se pudo obtener Usuario");
            }

            return usuarioBE;
        }
    }
}
