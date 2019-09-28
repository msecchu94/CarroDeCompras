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

namespace WebApp.Controllers
{

    [Authorize(Roles = "ADMIN")]
    public class ProductoController : Controller
    {
        private IProductoBLL _productoBLL;
        private IMarcaBLL _MarcaBLL;


        public ProductoController(IProductoBLL productoBLL, IMarcaBLL MarcaBLL)
        {
            this._productoBLL = productoBLL;
            this._MarcaBLL = MarcaBLL;
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
        //[ValidateAntiForgeryToken]

        [HttpGet]
        public ActionResult Crear()
        {
            Producto producto = new Producto();

            producto.Activo = true;


            try
            {
                var listamarcaDTO = _MarcaBLL.CargarMarca();
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
            #region File
            if (producto.File != null)
            {
                producto.SubirArchivo(producto);

                ViewBag.Message = "Archivo cargado exitosamente !!!";
                ModelState.Clear();
            }
            #endregion

            #region Mapeo
            ProductoDTO productoDTO = null;

            try
            {

                if (producto != null)
                {
                    productoDTO = new ProductoDTO()
                    {
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        IdMarca = producto.IdMarca,
                        PrecioUnitario = producto.PrecioUnitario,
                        UrlImange = producto.UrlImagen,
                        Activo = producto.Activo,

                    };

                    ViewBag.resultado = _productoBLL.AltaProducto(productoDTO);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ViewBag.resultado != true)
            {
                ViewBag.Mensaje = "Error al Insertar Registro";
            }
            ViewBag.Mensaje = "Se Registro Correctamente";
            return RedirectToAction("Index", "Producto");
            #endregion

        }

        [HttpGet]

        public ActionResult Editar(int codigo)
        {
            Producto producto = null;

            try
            {
                var productoDTO = _productoBLL.ObtenerPorId(codigo);
                producto = new Producto()
                {
                    Activo = productoDTO.Activo,
                    Descripcion = productoDTO.Descripcion,
                    Nombre = productoDTO.Nombre,
                    PrecioUnitario = productoDTO.PrecioUnitario,
                    Codigo = productoDTO.Codigo,
                    IdMarca = productoDTO.IdMarca,
                    UrlImagen = productoDTO.UrlImange
                };

            }
            catch (Exception)
            {
                throw;
            }
            ViewBag.lista = _MarcaBLL.CargarMarca();
            return View(producto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Producto producto)
        {
            if (producto.File != null)
            {
                producto.SubirArchivo(producto);
            }

            ProductoDTO productoDTO = null;
            try
            {
                productoDTO = new ProductoDTO()
                {
                    UrlImange = producto.UrlImagen,
                    Activo = producto.Activo,
                    Codigo = producto.Codigo,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    IdMarca = producto.IdMarca,
                    PrecioUnitario = producto.PrecioUnitario,
                };

                ViewBag.resultado = _productoBLL.EditarProducto(productoDTO);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ViewBag.resultado != true)
            {
                ViewBag.Mensaje = "Error al Insertar Registro";
            }
            ViewBag.Mensaje = "Se Registro Correctamente";
            return RedirectToAction("Index", "Producto");
        }

    }
}