using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.IO;
using AutoMapper;

namespace WebApp.Controllers
{

    [Authorize(Roles = "ADMIN")]

    public class ProductoController : Controller
    {
        private IProductoBLL _productoBLL;
        private IMarcaBLL _marcaBLL;

        public ProductoController(IProductoBLL ProductoBLL, IMarcaBLL MarcaBLL)
        {
            this._productoBLL = ProductoBLL;
            this._marcaBLL = MarcaBLL;
        }

        [HttpGet]
        public ActionResult Index(string mensaje)
        {
            ViewBag.Mensaje = mensaje;

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
                    UrlImange = item.UrlImange,
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

        [HttpGet]
        public ActionResult Crear()
        {
            Producto producto = new Producto();
            producto.Activo = true;

            try
            {
                var listamarcaDTO = _marcaBLL.CargarMarca();
                producto.ListaMarca = listamarcaDTO.Select(item => new Marca
                {
                    Id = item.Id,
                    Nombre = item.Nombre

                }).ToList();

                return View(producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Producto producto)
        {

            if (!ModelState.IsValid)
            {
                var listamarcaDTO = _marcaBLL.CargarMarca();
                producto.ListaMarca = listamarcaDTO.Select(item => new Marca
                {
                    Id = item.Id,
                    Nombre = item.Nombre

                }).ToList();
                return View("Crear", producto);
            }

            #region File



            if (producto.File != null)
            {
                producto.SubirArchivo(producto);

                ViewBag.Message = "Archivo cargado exitosamente !!!";
                ModelState.Clear();
            }

            #endregion

            try
            {
                var productoDTO = Mapper.Map<ProductoDTO>(producto);
                ViewBag.resultado = _productoBLL.AltaProducto(productoDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ViewBag.resultado != true)
            {
                return Json(new { Success = false });

            }
            return Json(new { Success = true });

        }

        [HttpGet]
        public ActionResult Editar(int codigo)
        {
            try
            {
                var productoDTO = _productoBLL.ObtenerPorId(codigo);
                var producto = Mapper.Map<Producto>(productoDTO);

                ViewBag.lista = _marcaBLL.CargarMarca();
                return View(producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.lista = _marcaBLL.CargarMarca();
                return View("Editar", producto);
            }

            if (producto.File != null)
            {
                producto.SubirArchivo(producto);
            }

            try
            {
                var productoDTO = Mapper.Map<ProductoDTO>(producto);
                ViewBag.resultado = _productoBLL.EditarProducto(productoDTO);

                if (ViewBag.resultado != true)
                {
                    return Json(new { Success = false });
                }
                return Json(new { Success = true });

            }
            catch (Exception ex)
            {
                return Json(new { Success = false });
                throw ex;
            }

        }
    }
}