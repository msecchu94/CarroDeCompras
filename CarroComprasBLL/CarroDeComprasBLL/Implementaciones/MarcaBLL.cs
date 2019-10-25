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

    public class MarcaBLL : IMarcaBLL
    {
        private readonly IRepositoryMarca _repositoryMarca;

        public MarcaBLL(IRepositoryMarca repositoryMarca)
        {
            this._repositoryMarca = repositoryMarca;
        }

        public IEnumerable<MarcaDTO> CargarMarcas()
        {
            var marcaBE = _repositoryMarca.CargarMarcas();
            var marcaDTO = marcaBE.Select(itemBE => new MarcaDTO
            {
                Id = itemBE.Id,
                Nombre = itemBE.Nombre
            });

            return marcaDTO;
        }

        public MarcaDTO ObtenerPorId(int codigo)
        {
            MarcaDTO marcaDTO = null;
            MarcaBE marcaBE = null;

            marcaBE = _repositoryMarca.ObtenerPorId(codigo);
            if (marcaBE != null)
            {
                marcaDTO = new MarcaDTO()
                {
                    Id = marcaBE.Id,
                    Nombre = marcaBE.Nombre

                };
            }
            return marcaDTO;
        }
    }
}
