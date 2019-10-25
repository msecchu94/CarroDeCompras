using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LoginModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public string Password { get; set; }

    }
}