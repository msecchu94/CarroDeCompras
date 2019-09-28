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
        public ActionResult Index(string mensaje)
        {
            ViewBag.Mensaje = mensaje;

            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(UsuarioModel usuariomodel)
        {
            string mensaje = "";

            if (!ModelState.IsValid)
            {

                return View("Index", usuariomodel);
            }

            #region mapeo
            UsuarioModel usuariomodelnuevo = null;
            UsuarioDTO usuarioDTOnuevo = null;

            try
            {
                UsuarioDTO usuarioDTO = new UsuarioDTO()
                {
                    Nombre = usuariomodel.Nombre,
                    Usuario = usuariomodel.Usuario,
                    Password = usuariomodel.Password,
                    Id = usuariomodel.Id,
                    IdRol = usuariomodel.IdRol,
                    Activo = usuariomodel.Activo,
                };

                var user = _usuarioBLL.ObtenerUsuario(usuarioDTO);
                usuarioDTOnuevo = user;

                usuariomodelnuevo = new UsuarioModel()
                {
                    Nombre = usuarioDTOnuevo.Nombre,
                    Usuario = usuarioDTOnuevo.Usuario,
                    Password = usuarioDTOnuevo.Password,
                    PasswordSalt = usuarioDTOnuevo.PasswordSalt,
                    Id = usuarioDTOnuevo.Id,
                    IdRol = usuarioDTOnuevo.IdRol,
                    Activo = usuarioDTOnuevo.Activo,

                };
            }
            catch (Exception)
            {

                mensaje = "El Usuario no se encuentra registrado";
                usuariomodel.Password = string.Empty;
                return RedirectToAction("Index", new { mensaje });
            }


            #endregion


            if (!usuariomodelnuevo.Activo)
            {
                mensaje = "Usuario sin acceso";
                usuariomodel.Password = string.Empty;
                return RedirectToAction("Index", new { mensaje });
            }

            if (usuariomodel.VerificarHashedPassword(usuariomodel.Password, usuariomodelnuevo.Password, usuarioDTOnuevo.PasswordSalt) == true)
            {
                usuariomodel.GenerarTicketCookie(Response, usuariomodelnuevo);

                if (usuariomodelnuevo.IdRol == "CLI")
                {
                    mensaje = "Bienvenido ";
                    return RedirectToAction("Index", "Catalogo", new { mensaje });
                }
                mensaje = "Bienvenido ";
                return RedirectToAction("Index", "Producto", new { mensaje });
            }

            else
            {
                mensaje = "Contraseña no válida";
                return RedirectToAction("Index", new { mensaje });
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

        [Authorize]
        public ActionResult LogOut()
        {
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

            //3

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
        public ActionResult ModificarPassword(string mensaje)
        {
            ViewBag.Mensaje = mensaje;

            if (mensaje == null)
            {
                UsuarioModel usuario = new UsuarioModel();
                usuario.Usuario = User.Identity.GetUserName();
                return View(usuario);
            }

            try
            {
                ViewBag.msjExito = TempData["msjExito"];
                return View("ModificarPassword");
            }
            catch (Exception)
            {
                ViewBag.msjError = TempData["msjError"];
                return RedirectToAction("ModificarPassword");
            }


        }

        [Authorize, HttpPost]
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
                //addmodelerror
                ModelState.AddModelError("Password", "No puede usar contraseña vigente");
                return View("ModificarPassword", usuarioModel);
            }

            if (usuarioModel.PasswordNueva ==null || usuarioModel.PasswordConfirmada == null)
            {
                 //addmodelerror

                ModelState.AddModelError("PasswordNueva", "El campo no puede estar vacio");
                ModelState.AddModelError("PasswordConfirmada", "El campo no puede estar vacio");

                usuarioModel.Nombre = string.Empty;
                usuarioModel.Nombre = string.Empty;
                usuarioModel.Password = string.Empty;

                return View("ModificarPassword", usuarioModel);
            }

            #region mapeo

            UsuarioDTO usuarioDTOverificar = null;
            UsuarioModel usuariomodelverificado = null;

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                Usuario = usuarioModel.Usuario,
            };

            try
            {
                usuarioDTOverificar = _usuarioBLL.ObtenerUsuario(usuarioDTO);

                usuariomodelverificado = new UsuarioModel()
                {
                    Password = usuarioDTOverificar.Password,
                    PasswordSalt = usuarioDTOverificar.PasswordSalt,
                };

                if (usuarioModel.VerificarHashedPassword(usuarioModel.Password, usuariomodelverificado.Password, usuariomodelverificado.PasswordSalt) == true)
                {
                    string passwordHash, passwordSalt;
                    usuarioModel.CreatePasswordHash(usuarioModel.PasswordConfirmada, out passwordHash, out passwordSalt);

                    usuarioDTO.Password = passwordHash;
                    usuarioDTO.PasswordSalt = passwordSalt;

                    _usuarioBLL.ModificarPassword(usuarioDTO);

                    TempData["msjExito"] = "Modificacion Exitosa !!";
                    ViewBag.msjExito = TempData["msjExito"];

                    mensaje = "Contraseña Modificada Exitosamente!";
                    return RedirectToAction("ModificarPassword", new { mensaje });
                }

                ModelState.AddModelError("Password", "La contraseña actual es incorrecta");
                return View("ModificarPassword", usuarioModel);
            }
            catch (Exception)
            {
                TempData["msjError"] = "Error al Modificacion Contraseña.";
                ViewBag.msjError = TempData["msjError"];

                return RedirectToAction("ModificarPassword");
            }


            #endregion



        }
    }
}
