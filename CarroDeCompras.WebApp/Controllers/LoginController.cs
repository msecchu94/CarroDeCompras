using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.IO;
using System.Data;
using CarroDeComprasCommon.Entidad;
using System.Web.Security;
using WebGrease;
using Microsoft.AspNet.Identity;
using System.Web.UI;
using AutoMapper;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioBLL _usuarioBLL;

        public LoginController(IUsuarioBLL usuarioBLL)
        {
            this._usuarioBLL = usuarioBLL;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Index(string mensaje, string permiso)
        {
            if (permiso != null)
            {
                ViewBag.Permiso = permiso;
                return View();
            }

            if (mensaje != null)
            {
                ViewBag.Mensaje = mensaje;
                TempData["msjExito"] = "Modificacion Exitosa,Valide su crave para continuar ... !!";
                ViewBag.msjExito = TempData["msjExito"];
            }

            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(UsuarioModel usuarioModel)
        {
            string permiso = "";

            if (!ModelState.IsValid)
            {
                return View("Index", usuarioModel);
            }

            try
            {
                var usuarioDTO = Mapper.Map<UsuarioDTO>(usuarioModel);   //MODELO DTO NVO
                var getUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO); //METODO DTO

                var usuarioResult = Mapper.Map<UsuarioModel>(getUsuario); //MODELO uSUARIO resultante

                if (!usuarioResult.Activo)
                {
                    permiso = "Usuario sin acceso";
                    usuarioModel.Password = string.Empty;
                    return RedirectToAction("Index", new { permiso });
                }

                if (usuarioModel.VerificarHashedPassword(usuarioModel.Password, usuarioResult.Password, usuarioResult.PasswordSalt) == true)
                {
                    usuarioModel.GenerarTicketCookie(Response, usuarioResult);

                    permiso = "Bienvenido ";
                    if (usuarioResult.IdRol == "CLI")
                    {
                        return RedirectToAction("Index", "Catalogo", new { mensaje = permiso });
                    }
                    return RedirectToAction("Index", "Producto", new { mensaje = permiso });
                }
                else
                {
                    permiso = "Contraseña no válida";
                    return RedirectToAction("Index", new { permiso });
                }

            }
            catch (Exception)
            {
                permiso = "El Usuario no se encuentra registrado";
                usuarioModel.Password = string.Empty;
                return RedirectToAction("Index", new { permiso });
            }

        }

        [Authorize]
        public ActionResult LogOut()
        {
            #region Opciones

            //1

            //FormsAuthentication.SignOut(); 
            //Response.Cookies.Remove(FormsAuthentication.FormsCookieName); 

            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1)); 
            //HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName]; 

            //if (cookie != null)
            //{ cookie.Expires = DateTime.Now.AddDays(-1); Response.Cookies.Add(cookie); }

            //2

            //FormsAuthentication.SignOut();
            //HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            //rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            //Response.Cookies.Add(rFormsCookie);

            #endregion

            FormsAuthentication.SignOut();
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);

            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpGet, Authorize]
        public ActionResult ModificarPassword()
        {
            UsuarioModel usuario = new UsuarioModel();
            usuario.Usuario = User.Identity.GetUserName();

            return View(usuario);
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificarPassword(UsuarioModel usuarioModel)
        {
            string mensaje = "";

            if (!ModelState.IsValid)
            {
                usuarioModel.Password = string.Empty;
                usuarioModel.PasswordNueva = string.Empty;

                usuarioModel.PasswordConfirmada = string.Empty;
                return View("ModificarPassword", usuarioModel);
            }

            if (usuarioModel.Password == usuarioModel.PasswordNueva)
            {
                ModelState.AddModelError("Password", "No puede usar contraseña vigente");
                return View("ModificarPassword", usuarioModel);
            }

            if (usuarioModel.PasswordNueva == null || usuarioModel.PasswordConfirmada == null)
            {
                ModelState.AddModelError("PasswordNueva", "El campo no puede estar vacio");
                ModelState.AddModelError("PasswordConfirmada", "El campo no puede estar vacio");

                usuarioModel.Nombre = string.Empty;
                usuarioModel.Nombre = string.Empty;
                usuarioModel.Password = string.Empty;

                return View("ModificarPassword", usuarioModel);
            }

            try
            {
                var usuarioDTO = Mapper.Map<UsuarioDTO>(usuarioModel);
                var getUsuario = _usuarioBLL.ObtenerUsuario(usuarioDTO);

                var usuarioResult = Mapper.Map<UsuarioModel>(getUsuario);

                if (usuarioModel.VerificarHashedPassword(usuarioModel.Password, usuarioResult.Password, usuarioResult.PasswordSalt) == true)
                {
                    string passwordHash, passwordSalt;
                    usuarioModel.CreatePasswordHash(usuarioModel.PasswordConfirmada, out passwordHash, out passwordSalt);

                    usuarioDTO.Password = passwordHash;
                    usuarioDTO.PasswordSalt = passwordSalt;

                    _usuarioBLL.ModificarPassword(usuarioDTO);

                    mensaje = "Contraseña Modificada Exitosamente!";

                    TempData["msjExito"] = "Modificacion Exitosa, Valide su clave Nuevamente ...";
                    ViewBag.msjExito = TempData["msjExito"];

                    return View("Index");
                }

                ModelState.AddModelError("Password", "La contraseña actual es incorrecta");
                return View("ModificarPassword", usuarioModel);
            }
            catch (Exception)
            {
                TempData["msjError"] = "Error al Modificacion Contraseña.";
                ViewBag.msjError = TempData["msjError"];

                return View("ModificarPassword");
            }
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Registro()
        {
            try
            {
                ViewBag.msjExito = TempData["msjExito"];
            }
            catch (Exception)
            {
                ViewBag.msjError = TempData["msjError"];
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(UsuarioModel usuariomodel)
        {
            string mensaje = "";

            UsuarioDTO usuarioDTO = null;
            string passwordHash, passwordSalt;


            if (!ModelState.IsValid)
            {
                return View("Registro", usuariomodel);
            }

            if (usuariomodel.Apellido == null || usuariomodel.Nombre == null)
            {
                ModelState.AddModelError("Nombre", "El campo Nombre no puede estar vacio");
                ModelState.AddModelError("Apellido", "El campo Apellido no puede estar vacio");

                usuariomodel.Nombre = string.Empty;
                usuariomodel.Nombre = string.Empty;
                usuariomodel.Password = string.Empty;

                return View("Registro", usuariomodel);
            }

            usuariomodel.CreatePasswordHash(usuariomodel.Password, out passwordHash, out passwordSalt);

            usuariomodel.PasswordSalt = passwordSalt;
            usuariomodel.Password = passwordHash;

            try
            {
                usuarioDTO = new UsuarioDTO()
                {
                    Usuario = usuariomodel.Usuario,
                    Nombre = usuariomodel.Nombre,
                    Apellido = usuariomodel.Apellido,
                    Password = usuariomodel.Password,
                    PasswordSalt = usuariomodel.PasswordSalt,
                    Activo = true,
                    IdRol = "CLI"
                };

                var resultado = _usuarioBLL.AltaUsuario(usuarioDTO);

                TempData["msjExito"] = "Registro Exitoso !!";
                ViewBag.msjExito = TempData["msjExito"];

                return RedirectToAction("Registro");
            }
            catch (Exception)
            {
                TempData["msjError"] = "Error al registrar Usuario.";
                ViewBag.msjError = TempData["msjError"];

                usuariomodel.Password = string.Empty;
                return RedirectToAction("Registro");
            }

        }
    }
}
