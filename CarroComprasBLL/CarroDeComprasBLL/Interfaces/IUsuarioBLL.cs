using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Interfaces
{
    public interface IUsuarioBLL
    {

        UsuarioDTO ObtenerUsuario(UsuarioDTO usuarioDTO);
        UsuarioDTO AltaUsuario(UsuarioDTO usuarioDTO);
        UsuarioDTO ModificarPassword(UsuarioDTO usuarioDTO);
    }
}
