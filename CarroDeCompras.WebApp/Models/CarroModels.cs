using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Carro
    {
        public IEnumerable<CarroModels> ListaCarro { get; set; }
    }

    public class CarroModels
    {
        public CarroModels() { }
        public CarroModels(CarroModels CarroModels)
        {
            this.Producto.Codigo = CarroModels.Producto.Codigo;
            this.Producto.Nombre = CarroModels.Producto.Nombre;
            this.Cantidad = CarroModels.Cantidad;
        }

        public Producto Producto { get; set; }

        public int IdUsuario { get; set; }

        [Range(0, 15, ErrorMessage = "rango incorrecto")]
        public int Cantidad { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //[DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Total
        {get;set;}

        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }
    }
}