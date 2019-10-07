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
            var usuarioBE = Mapper.Map<UsuarioBE>(usuarioDTO);
            var getModificacion = _repositoryUsuario.ModificarPassword(usuarioBE);

            var modificacionResult = Mapper.Map<UsuarioDTO>(getModificacion);

            return modificacionResult;
        }

    }
}

