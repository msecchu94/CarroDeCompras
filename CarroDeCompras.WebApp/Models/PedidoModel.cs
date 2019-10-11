using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PedidoModel
    {
        public int IdUsuario { get; set; }

        public int NumeroPedido { get; set; }

        public int CodigoCliente { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        public IEnumerable<DetallePedidoModel> DetallesPedido { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Total
        {
            get
            {
                if (DetallesPedido != null)
                {
                    return DetallesPedido.Sum(x => x.SubTotal);
                }
                else { return 0; }
            }

        }

    }

    public class DetallePedidoModel
    {
        public int NumeroPedido { get; set; }

        public int NumeroItem { get; set; }

        public int Cantidad { get; set; }

        public int CodigoProducto { get { if (Producto != null) { return Producto.Codigo; } else { return 0; } } }

        public decimal PrecioUnitario { get { if (Producto != null) { return Producto.PrecioUnitario; } else { return 0; } } }

        public Producto Producto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal SubTotal
        { get { if (Producto != null) { return Producto.PrecioUnitario * Cantidad; } else { return 0; } } }
    }

}