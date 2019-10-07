using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

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
            try
            {
                var usuarioBE = Mapper.Map<UsuarioBE>(usuarioDTO);
                var result = _repositoryUsuario.AltaUsuario(usuarioBE);
            }
            catch (Exception)
            {
                throw;
            }

            return usuarioDTO;
        }

        public UsuarioDTO ObtenerUsuario(UsuarioDTO usuarioDTO)
        {

            try
            {

                var usuarioBE = Mapper.Map<UsuarioBE>(usuarioDTO);
                var getUsuario = _repositoryUsuario.ObtenerUsuario(usuarioBE);

                var usuarioResult = Mapper.Map<UsuarioDTO>(getUsuario);
                return usuarioResult;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public UsuarioDTO ModificarPassword(UsuarioDTO usuarioDTO)
        {
            //UsuarioDTO usuarioDTOverificar = null;

            ////UsuarioBE usuarioBE = null;
            //UsuarioBE usuarioBEverificar = null;

            var usuarioBE = Mapper.Map<UsuarioBE>(usuarioDTO);
            //   new UsuarioBE()
            //{
            //    Usuario = usuarioDTO.Usuario,
            //    Password = usuarioDTO.Password,
            //    PasswordSalt = usuarioDTO.PasswordSalt
            //};

            var getModificacion = _repositoryUsuario.ModificarPassword(usuarioBE);
            var modificacionResult = Mapper.Map<UsuarioDTO>(getModificacion);

            //usuarioDTOverificar = new UsuarioDTO()
            //{
            //    Password = getModificacion.Password,
            //    PasswordSalt = getModificacion.PasswordSalt
            //};
            return modificacionResult;
        }

    }
}

