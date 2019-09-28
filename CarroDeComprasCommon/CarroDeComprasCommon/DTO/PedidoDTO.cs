using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.DTO
{
    public class PedidoDTO
    {
        public int IdUsuario { get; set; }

        public int NumeroPedido { get; set; }

        public int CodigoCliente { get; set; }

        public DateTime Fecha { get; set; }

        public string Observacion { get; set; }

        public IEnumerable<DetallePedidoDTO> ListaCarro { get; set; }

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


    public class DetallePedidoDTO
    {
        public int NumeroPedido { get; set; }

        public int NumeroItem { get; set; }

        public int Cantidad { get; set; }


        public int CodigoProducto { get { if (ProductoDTO != null) { return ProductoDTO.Codigo; } else { return 0; } } }
        public decimal PrecioUnitario { get { if (ProductoDTO != null) { return ProductoDTO.PrecioUnitario; } else { return 0; } } }

        public ProductoDTO ProductoDTO { get; set; }

        public decimal SubTotal { get { if (ProductoDTO != null) { return ProductoDTO.PrecioUnitario * Cantidad; } else { return 0; } } }
    }
}
