using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ModificarContraseña
    {
       
        public string Password { get; set; }

        public string NuevaPassword { get; set; }

        public string ConfirmarPassword { get; set; }
    }
}