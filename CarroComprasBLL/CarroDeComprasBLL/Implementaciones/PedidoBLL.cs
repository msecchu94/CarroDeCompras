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


        public PedidoBLL(IRepositoryProducto RepositoryProducto, IRepositoryPedido RepositoryPedido)
        {
            this._repositoryProducto = RepositoryProducto;
            this._repositoryPedido = RepositoryPedido;
        }


        public bool AgregarPedido(PedidoDTO pedidoDTO)
        {
            var res = false;

            #region mapeo index-item
            int index = 1;

            foreach (var item in pedidoDTO.ListaCarro)
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

        public PedidoDTO ObtenerPedidos(int id)
        {
            var pedidoBE = _repositoryPedido.ObtenerPedidos(id);

            var pedidoDTO = Mapper.Map<PedidoDTO>(pedidoBE);

            return pedidoDTO;
        }
    }
}
