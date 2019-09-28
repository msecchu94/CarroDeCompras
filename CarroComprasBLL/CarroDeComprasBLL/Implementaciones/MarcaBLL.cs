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

        public IEnumerable<MarcaDTO> CargarMarca()
        {

            try
            {
                var marcaBE = _repositoryMarca.CargarMarca();
                var marcaDTO = marcaBE.Select(itemBE => new MarcaDTO
                      {
                          Id = itemBE.Id,
                          Nombre=itemBE.Nombre
                      });

                return marcaDTO;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public MarcaDTO ObtenerPorId(int codigo)
        {
            MarcaDTO marcaDTO = null;
            MarcaBE marcaBE = null;


            try
            {
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
