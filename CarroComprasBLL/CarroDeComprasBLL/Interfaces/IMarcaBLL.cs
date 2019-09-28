using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Interfaces
{
    public interface IMarcaBLL
    {
      IEnumerable <MarcaDTO> CargarMarca();

        MarcaDTO ObtenerPorId(int Codigo);

    }
}
