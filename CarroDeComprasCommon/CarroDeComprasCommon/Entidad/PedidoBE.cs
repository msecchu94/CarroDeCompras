using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.Entidad
{
    public class PedidoBE
    {

        public int IdUsuario { get; set; }

        public int NumeroPedido { get; set; }

        public int CodigoCliente { get; set; }

        public DateTime Fecha { get; set; }

        public string Observacion { get; set; }

        public IEnumerable<DetallePedidoBE> ListaCarro { get; set; }

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

    public class DetallePedidoBE
    {

        public int NumeroPedido { get; set; }

        public int NumeroItem { get; set; }

        public int Cantidad { get; set; }

        public int CodigoProducto => Producto?.Codigo ?? 0;

        public decimal PrecioUnitario => Producto?.PrecioUnitario ?? 0;

        public ProductoBE Producto { get; set; }

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
