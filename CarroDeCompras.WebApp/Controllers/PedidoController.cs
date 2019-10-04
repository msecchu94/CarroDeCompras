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

            var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);
            var getPedidos = _pedidoBLL.ObtenerPedidos(getIdUsuario.Id);

            var pedidoModel = Mapper.Map<IEnumerable<PedidoModel>>(getPedidos);

            return View(pedidoModel);
        }

        [HttpPost]
        public ActionResult CargarPedido(string Observaciones)
        {
            var observaciones = Convert.ToString(Observaciones);

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };

            var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);
            var getcarro = _carroBLL.ObtenerCarro(getIdUsuario.Id);

            var pedidoModels = Mapper.Map<PedidoModel>(getcarro);
            pedidoModels.Observacion = observaciones;

            pedidoModels.Fecha = DateTime.Now;
            pedidoModels.IdUsuario = getIdUsuario.Id;

            var pedidoDTO = Mapper.Map<PedidoDTO>(pedidoModels);

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