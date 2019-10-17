using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using CarroDeComprasCommon.Entidad;
using System.Web.Security;
using AutoMapper;

namespace WebApp.Controllers
{
    public class CarroController : Controller
    {
        private IProductoBLL _productoBLL;
        private ICarroBLL _carroBLL;
        private IUsuarioBLL _usuarioBLL;

        public CarroController(IProductoBLL productoBLL, ICarroBLL carroBLL, IUsuarioBLL usuarioBLL)
        {
            this._productoBLL = productoBLL;
            this._carroBLL = carroBLL;
            this._usuarioBLL = usuarioBLL;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index(string permiso)
        {
            if (permiso != null)
            {
                ViewBag.Permiso = permiso;
                return View();
            }

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };

            var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);
            var getcarro = _carroBLL.ObtenerCarro(getIdUsuario.Id);

            var pedidoModels = Mapper.Map<PedidoModel>(getcarro);

            return View(pedidoModels);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Agregar(string cantidad, string codigoProducto)
        {
            if (User.IsInRole("ADMIN"))
            {
                string permiso = "Permiso Restringido, funcionalidad solo para Clientes";
                return View("Index", permiso);
            }

            var _codigoProducto = Convert.ToInt32(codigoProducto);
            var cantidadProducto = Convert.ToInt32(cantidad);

            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Usuario = User.Identity.Name;

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = usuarioModel.Usuario
            };
            var getIdUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);

            #region sumar item

            var carroDTO = _carroBLL.ObtenerCarro(getIdUsuario.Id);
            var carroModel = Mapper.Map<PedidoBE>(carroDTO);

            foreach (var item in carroModel.DetallesPedido)
            {
                carroModel.DetallesPedido.First(x => x.CodigoProducto == _codigoProducto).Cantidad = cantidadProducto;
            }

            #endregion

            try
            {
                _carroBLL.AgregarCarro(_codigoProducto, cantidadProducto, getIdUsuario.Id);
                return Json(new { Success = true });
            }
            catch (Exception)
            {
                return Json(new { Success = false });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EliminarItem(int codigoProducto)
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = User.Identity.Name
            };

            var getid = _usuarioBLL.ObtenerUsuario(usuarioDTO);

            try
            {
                _carroBLL.EliminarItem(codigoProducto, getid.Id);
                return Json(new { Success = true });
            }
            catch (Exception)
            {
                return Json(new { Success = false });
            }
        }
    }
}
