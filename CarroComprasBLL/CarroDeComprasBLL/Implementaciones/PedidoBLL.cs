using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using CarroDeComprasDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarroDeComprasCommon.Entidad;

namespace CarroDeComprasBLL.Implementaciones
{
    public class PedidoBLL : IPedidoBLL
    {
        private readonly IRepositoryProducto _repositoryProducto;
        private readonly IRepositoryPedido _repositoryPedido;
        
        public PedidoBLL(IRepositoryProducto RepositoryProducto,IRepositoryPedido RepositoryPedido)
        {
            this._repositoryProducto = RepositoryProducto;
            this._repositoryPedido = RepositoryPedido;
        }
        
        public bool AgregarPedido(PedidoDTO pedidoDTO)
        {
            var res = false;

            #region mapeo index-item

            int index = 1;

            foreach (var item in pedidoDTO.DetallesPedido)
            {
                item.NumeroItem = index;
                index++;
            };
            #endregion

            var pedidoBE = Mapper.Map<PedidoBE>(pedidoDTO);

            try
            {
                _repositoryPedido.AgregarPedido(pedidoBE);
                return res = true;
            }
            catch (Exception)
            {
                return res = false;
            }

        }

        public IEnumerable<PedidoDTO> ObtenerPedidos()
        {
            var getpedidoBE = _repositoryPedido.ObtenerPedidos();

            var pedidoDTO = Mapper.Map<IEnumerable<PedidoDTO>>(getpedidoBE);

            return pedidoDTO;
        }

        public IEnumerable<PedidoDTO> ObtenerPedidosXusuario(int idUsuario)
        {
            var getpedidosBE = _repositoryPedido.ObtenerPedidosXusuario(idUsuario);

            var pedidosDTO = Mapper.Map<IEnumerable<PedidoDTO>>(getpedidosBE);

            return pedidosDTO;
        }

        public PedidoDTO ObtenerPedido(int numPedido)
        {
            var getpedidoBE = _repositoryPedido.ObtenerPedido(numPedido);
            var pedidoDTO = Mapper.Map<PedidoDTO>(getpedidoBE);

            return pedidoDTO;

        }
    }
}
