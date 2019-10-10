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

        public ActionResult Index()
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };

            if (User.IsInRole("ADMIN"))
            {
                var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);
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
            //var carro = Mapper.Map<CarroModels>(getcarro);

            foreach (var item in pedidoModel.DetallesPedido)
            {
                getcarro.DetallesPedido.First().Cantidad = pedidoModel.DetallesPedido.First().Cantidad;

                    }

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
    }
}