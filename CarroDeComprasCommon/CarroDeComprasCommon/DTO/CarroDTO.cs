using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.DTO
{
    public class CarroModelsDTO
    {

       

    }


    public class CarroDTO
    {

        public IEnumerable<CarroDTO> ListaCarro { get; set; }
        public ProductoDTO ProductoDTO { get; set; }

        public int IdUsuario { get; set; }

        public int Cantidad { get; set; }

        

        public decimal Subtotal
        {
            get
            ;
            set
           ;
        }

        public decimal Total
        {
            get
            {
                if (ListaCarro != null)
                {
                    return ListaCarro.Sum(d => d.Subtotal);
                }
                else { return 0; }
            } 

        }

        public string Observaciones { get; set; }
    }
}
