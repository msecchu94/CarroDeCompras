using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.Entidad
{
    public class CarroBE
    {
        public IEnumerable<CarroBE> ListaCarro { get; set; }

        public ProductoBE ProductoBE { get; set; }

        public int IdUsuario { get; set; }

        public int Cantidad { get; set; }

        public string Observaciones { get; set; }

        public decimal Subtotal
        {
            get
            {
                if (ProductoBE != null)
                {
                    return ProductoBE.PrecioUnitario * Cantidad;
                }
                else { return 0; }
            }
            set
            {

            }
        }

        public decimal Total
        {

            get
            {
                if (ProductoBE != null)
                {
                    return ListaCarro.Sum(x=>x.Subtotal);
                }
                else { return 0; }
            
            }
            set { }

        }

    }




}
