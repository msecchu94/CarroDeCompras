using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MarcaModel
    {
        public IEnumerable<Marca> ListaDeMarcas { get; set; }
    }

    public class Marca 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Debe contener un rango minimo de 3 digitos")]
        public string Nombre { get; set; }
    }
}