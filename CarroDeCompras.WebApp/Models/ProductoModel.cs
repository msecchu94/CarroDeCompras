using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class ProductoModel
    {
        public IEnumerable<Producto> ListaDeProductos { get; set; }
        public IEnumerable<Marca> ListaMarca { get; set; }
    }

    public class Producto
    {
        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Debe contener un rango minimo de 4 digitos")]
        [DisplayName("Producto")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Debe contener un rango minimo de 4 digitos")]
        public string Descripcion { get; set; }

        //[Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = "Marca")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [DataType(DataType.Upload, ErrorMessage = "el formato no es valido")]
        public string UrlImagen { get; set; }

        //[DataType()]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = "Estado")]
        public bool Activo { get; set; }

        public Marca Marca { get; set; }

        public IEnumerable<Marca> ListaMarca { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Archivo")]
        [Required(ErrorMessage = "seleccione archivo")]
        public HttpPostedFileBase File { get; set; }

        public void SubirArchivo(Producto producto)
        {
            string fileName = Path.GetFileNameWithoutExtension(producto.File.FileName);
            string extension = Path.GetExtension(producto.File.FileName);

            fileName = fileName + extension;
            producto.UrlImagen = "~/Images/Productos/" + fileName;

            fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Productos/"), fileName);
            producto.File.SaveAs(fileName);

            //ModelState.Clear();

            //var fileSize = producto.File.ContentLength;
            //WebImage photo = WebImage.GetImageFromRequest();
            //string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);
            //UrlImagen = @"~\Images\Productos\" + fileName;
            //photo.Save(UrlImagen);
            //string path = HttpContext.Current.Server.MapPath(@"~\Images\Productos\");

            //string fullPath = Path.Combine(path, fileName);

            //File.SaveAs(fullPath);
            //UrlImagen = path + fileName;

            //if (photo != null)
            //{
            //    string newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(photo.FileName);

            //}
        }
    }

}