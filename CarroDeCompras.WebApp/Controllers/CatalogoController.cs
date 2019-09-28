using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Web.Helpers;
using System.Web.Services;
using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;

namespace WebApp.Controllers
{

    public class CatalogoController : Controller
    {
        private IProductoBLL _productoBLL;
        private IMarcaBLL _MarcaBLL;


        public CatalogoController(IProductoBLL productoBLL, IMarcaBLL MarcaBLL, IPedidoBLL PedidoBLL)
        {
            this._productoBLL = productoBLL;
            this._MarcaBLL = MarcaBLL;
        }

        [Authorize]
        public ActionResult Index(string mensaje)
        {
            ViewBag.Mensaje = mensaje;

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }

            ProductoModel productoModel = new ProductoModel();
            try
            {
                var productoDTO = _productoBLL.ObtenerProductos();
                productoModel.ListaDeProductos = productoDTO.Select(item => new Producto
                {
                    Activo = item.Activo,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    IdMarca = item.IdMarca,
                    Nombre = item.Nombre,
                    PrecioUnitario = item.PrecioUnitario,
                    UrlImagen = item.UrlImange,
                    Marca = new Marca
                    {
                        Id = item.Marca.Id,
                        Nombre = item.Marca.Nombre
                    }

                });

                return View(productoModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}