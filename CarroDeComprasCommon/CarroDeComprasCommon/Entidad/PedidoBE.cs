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

        public IEnumerable<DetallePedidoBE> DetallesPedido { get; set; }

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

    public class DetallePedidoBE
    {

        public int NumeroPedido { get; set; }

        public int NumeroItem { get; set; }

        public int Cantidad { get; set; }


        public ProductoBE ProductoBE { get; set; }

        public int CodigoProducto { get { if (ProductoBE != null) { return ProductoBE.Codigo; } else { return 0; } } }

        public decimal PrecioUnitario { get { if (ProductoBE != null) { return ProductoBE.PrecioUnitario; } else { return 0; } } }
        public decimal SubTotal { get { if (ProductoBE != null) { return ProductoBE.PrecioUnitario * Cantidad; } else { return 0; } } }
    }
}
