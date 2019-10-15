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
using System.Web.UI.WebControls;
using System.Configuration;

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
        [DisplayName("Código")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Debe contener un rango minimo de 4 digitos")]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Debe contener un rango minimo de 4 digitos")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        //[Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = "Marca")]
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.Upload, ErrorMessage = "el formato no es valido")]
        public string UrlImange { get; set; }


        [Display(Name = "Archivo")]
        //[Required(ErrorMessage = "seleccione archivo")]
        public HttpPostedFileBase File { get; set; }

        //[DataType()]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = "Estado")]
        public bool Activo { get; set; }

        public Marca Marca { get; set; }

        public IEnumerable<Marca> ListaMarca { get; set; }

        public void SubirArchivo(Producto producto)
        {

            ////Use Namespace called :  System.IO             objeto 
            //string FileName = Path.GetFileNameWithoutExtension(producto.File.FileName);

            ////To Get File Extension  
            //string FileExtension = Path.GetExtension(producto.File.FileName);

            ////Add Current Date To Attached File Name  
            //FileName = FileName.Trim() + FileExtension;

            ////Get Upload path from Web.Config file AppSettings.  
            //string UploadPath = ConfigurationManager.AppSettings["ProductosUrlImagen"].ToString();

            ////Its Create complete path to store in server.  
            //producto.UrlImange = UploadPath + FileName;

            ////To copy and save file into server.  
            //producto.File.SaveAs(producto.UrlImange);


            //To save Club Member Contact Form Detail to database table.  

            //try
            //{
            //    string path = HttpContext.Current.Server.MapPath("~/Images/Productos/");
            //    if (!Directory.Exists(path))
            //    {
            //        Directory.CreateDirectory(path);
            //    }
            //    File.SaveAs(path + Path.GetFileName(File.FileName));
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


            string fileName = Path.GetFileNameWithoutExtension(producto.File.FileName);
            string extension = Path.GetExtension(producto.File.FileName);

            fileName = fileName + extension;
            producto.UrlImange = ConfigurationManager.AppSettings["ProductosUrlImagen"].ToString() + fileName;

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