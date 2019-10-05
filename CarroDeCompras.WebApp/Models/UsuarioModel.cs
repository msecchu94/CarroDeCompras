using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace WebApp.Models
{
    public class UsuarioModel
    {
        public UsuarioModel() { }
        public UsuarioModel(UsuarioModel usuarioModel)
        {
            this.Usuario = usuarioModel.Usuario;
            this.Password = usuarioModel.Password;
        }

        public int Id { get; set; }
        public string IdRol { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public string Usuario { get; set; }


        public string Nombre { get; set; }
         
        public string Apellido { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("PasswordConfirmada", ErrorMessage = "Las contraseñas no coincide")]

        public string PasswordNueva { get; set; }

        [DataType(DataType.Password)]
        [Compare("PasswordNueva", ErrorMessage = "Las contraseñas no coincide")]

        public string PasswordConfirmada { get; set; }

        public string PasswordSalt { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }



        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }



        public bool VerificarHashedPassword(string password, string hashedPasswordBase64, string saltBase64)
        {
            var salt = Convert.FromBase64String(saltBase64);// string de salt obtenido de db transf bytes[]

            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // hashea pass ingresado  con salt 
                var computedHashBase64 = Convert.ToBase64String(computedHash);// convierte this  hash en string 

                return hashedPasswordBase64 == computedHashBase64;//compara password hasheada de db con hash de contraseña ingresado
            }
        }


        public void GenerarTicketCookie(HttpResponseBase response, UsuarioModel usuariomodel)
        {
            // generar ticket
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                version: 1,
                name: usuariomodel.Usuario,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddMinutes(20),
                isPersistent: false,
                userData: usuariomodel.IdRol.ToString(), //chequear el user data
                cookiePath: "/");

            // guardar cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            response.Cookies.Add(cookie);
        }

    }
}


