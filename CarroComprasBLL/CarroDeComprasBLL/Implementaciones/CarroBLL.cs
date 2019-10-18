using AutoMapper;
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
    public class CarroBLL : ICarroBLL
    {
        private IRepositoryCarro _repositoryCarro;
        private IProductoBLL _productoBLL;


        public CarroBLL(IRepositoryCarro repositoryCarro, IProductoBLL productoBLL)
        {
            this._repositoryCarro = repositoryCarro;
            this._productoBLL = productoBLL;
        }
        public void AgregarCarro(int _codigoProducto, int cantidadProducto, int idUsuario)
        {
            _repositoryCarro.AgragarCarro(_codigoProducto, cantidadProducto, idUsuario);
        }
        public PedidoDTO ObtenerCarro(int IdUsuario)
        {
            var carroBE = _repositoryCarro.ObtenerCarro(IdUsuario);
            var pedidoDTO = Mapper.Map<PedidoDTO>(carroBE);

            return pedidoDTO;
        }
        public void VaciarCarro(int IdUsuario)
        {
            _repositoryCarro.VaciarCarro(IdUsuario);
        }
        public void EliminarItem(int codigoProducto, int IdUsuario)
        {
            _repositoryCarro.EliminarItem(codigoProducto, IdUsuario);
        }
        public void ModificarCarro(int _codigoProducto, int suma)
        {
           _repositoryCarro.ModificarCarro(_codigoProducto,suma);
        }
    }
}
