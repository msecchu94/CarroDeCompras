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

        public DateTime Fecha { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observacion { get; set; }

        public IEnumerable<DetallePedidoModel> ListaCarro { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Total
        {
            get
            {
                if (ListaCarro != null)
                {
                    return ListaCarro.Sum(x => x.SubTotal);
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

        public int CodigoProducto => Producto?.Codigo ?? 0;

        public decimal PrecioUnitario => Producto?.PrecioUnitario ?? 0;

        public Producto Producto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal SubTotal
        {
            get
            {

                if (Producto != null)
                {
                    return Producto.PrecioUnitario * Cantidad;
                }
                else { return 0; }

            }
        }
    }

}