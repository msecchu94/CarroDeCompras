using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Implementaciones
{
    public class UsuarioBLL : IUsuarioBLL
    {
        private IRepositoryUsuario _repositoryUsuario;

        public UsuarioBLL(IRepositoryUsuario repositoryUsuario)
        {
            this._repositoryUsuario = repositoryUsuario;

        }

        public UsuarioDTO AltaUsuario(UsuarioDTO usuarioDTO)
        {
            UsuarioBE usuarioBE = null;
            if (usuarioDTO != null)
            {
                usuarioBE = new UsuarioBE()
                {
                    Usuario = usuarioDTO.Usuario,
                    Nombre = usuarioDTO.Nombre,
                    Apellido = usuarioDTO.Apellido,
                    PasswordSalt = usuarioDTO.PasswordSalt,
                    Password = usuarioDTO.Password,
                    Activo = usuarioDTO.Activo,
                    IdRol = usuarioDTO.IdRol

                };
            }

            //metodoINSERT
            var result = _repositoryUsuario.AltaUsuario(usuarioBE);

            return usuarioDTO;
        }


        public UsuarioDTO ObtenerUsuario(UsuarioDTO usuarioDTO)
        {
            UsuarioDTO usuarioDTOnuevo = null;

            UsuarioBE usuarioBE = null;
            UsuarioBE usuarioBENuevo = null;

                usuarioBE = new UsuarioBE()
                {
                    Nombre = usuarioDTO.Nombre,
                    Usuario = usuarioDTO.Usuario,
                    Password = usuarioDTO.Password,
                    Id = usuarioDTO.Id,
                    IdRol = usuarioDTO.IdRol,
                    Activo = usuarioDTO.Activo
                };
               
                var user = _repositoryUsuario.ObtenerUsuario(usuarioBE);


                usuarioBENuevo = user;
                usuarioDTOnuevo = new UsuarioDTO()
                {

                    Nombre = usuarioBENuevo.Nombre,
                    Usuario = usuarioBENuevo.Usuario,
                    Password = usuarioBENuevo.Password,
                    PasswordSalt = usuarioBENuevo.PasswordSalt,
                    Id = usuarioBENuevo.Id,
                    IdRol = usuarioBENuevo.IdRol,
                    Activo = usuarioBENuevo.Activo

                };
    
            return usuarioDTOnuevo;

        }

        public UsuarioDTO ModificarPassword(UsuarioDTO usuarioDTO)
        {
            UsuarioDTO usuarioDTOverificar = null;

            UsuarioBE usuarioBE = null;
            UsuarioBE usuarioBEverificar = null;

            usuarioBE = new UsuarioBE()
            {
                Usuario = usuarioDTO.Usuario,
                Password = usuarioDTO.Password,
                PasswordSalt = usuarioDTO.PasswordSalt
            };

            usuarioBEverificar = _repositoryUsuario.ModificarPassword(usuarioBE);

            usuarioDTOverificar = new UsuarioDTO()
            {
                Password = usuarioBEverificar.Password,
                PasswordSalt = usuarioBEverificar.PasswordSalt
            };
            return usuarioDTOverificar;

        }

    }
}

