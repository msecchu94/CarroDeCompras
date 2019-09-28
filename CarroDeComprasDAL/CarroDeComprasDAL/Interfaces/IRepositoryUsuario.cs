using CarroDeComprasCommon.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasDAL.Interfaces
{
    public interface IRepositoryUsuario
    {
        UsuarioBE ObtenerUsuario(UsuarioBE usuarioBE);

        UsuarioBE AltaUsuario(UsuarioBE usuarioBE);

        UsuarioBE ModificarPassword(UsuarioBE usuarioBE);

    }
}
