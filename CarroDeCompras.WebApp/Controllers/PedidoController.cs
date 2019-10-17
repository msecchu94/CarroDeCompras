using AutoMapper;
using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly IProductoBLL _productoBLL;
        private readonly ICarroBLL _carroBLL;
        private readonly IUsuarioBLL _usuarioBLL;
        private readonly IPedidoBLL _pedidoBLL;

        public PedidoController(IProductoBLL ProductoBLL, ICarroBLL CarroBLL, IUsuarioBLL UsuarioBLL, IPedidoBLL PedidoBLL)
        {
            this._productoBLL = ProductoBLL;
            this._carroBLL = CarroBLL;
            this._usuarioBLL = UsuarioBLL;
            this._pedidoBLL = PedidoBLL;
        }

        [HttpGet,]
        public ActionResult Index()
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };

            if (User.IsInRole("ADMIN"))
            {
                //var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);
                var getPedidos = _pedidoBLL.ObtenerPedidos();
                var pedidoModel = Mapper.Map<IEnumerable<PedidoModel>>(getPedidos);

                return View(pedidoModel);
            }
            else
            {
                var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);
                var getPedidos = _pedidoBLL.ObtenerPedidosXusuario(getIdUsuario.Id);

                var pedidoModel = Mapper.Map<IEnumerable<PedidoModel>>(getPedidos);

                return View(pedidoModel);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargarPedido(PedidoModel pedidoModel)
        {
            if (pedidoModel == null)
            {
                return View("Index");
            }

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };

            var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);

            //obtener carro
            var getcarro = _carroBLL.ObtenerCarro(getIdUsuario.Id);

            var carro = Mapper.Map<PedidoModel>(getcarro);

            // asignar perecio unitario desde el carro al pedido (x codigo del Producto)
            foreach (var item in pedidoModel.DetallesPedido)
            {

                item.Producto.PrecioUnitario = carro.DetallesPedido.First(x => x.Producto.Codigo == item.Producto.Codigo).Producto.PrecioUnitario;

            }

            pedidoModel.Fecha = DateTime.Now;
            pedidoModel.IdUsuario = getIdUsuario.Id;

            var pedidoDTO = Mapper.Map<PedidoDTO>(pedidoModel);

            try
            {
                if (_pedidoBLL.AgregarPedido(pedidoDTO))
                {
                    _carroBLL.VaciarCarro(getIdUsuario.Id);
                }

                return Json(new { Success = true });
            }
            catch (Exception)
            {
                return Json(new { Success = false });
            }

        }
        
        [HttpPost]
        public ActionResult _ModalPedido(int numeroPedido)
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };
            var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);

            var pedidoDTO = _pedidoBLL.ObtenerPedido(numeroPedido);
            var pedidoModel = Mapper.Map<PedidoModel>(pedidoDTO);

            return PartialView("_ModalPedido",pedidoModel);
        }
    }
}