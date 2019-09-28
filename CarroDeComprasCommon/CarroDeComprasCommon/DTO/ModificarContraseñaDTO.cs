using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.DTO
{
    public class ModificarContraseñaDTO
    {
        public string Password { get; set; }

        public string NuevaPassword { get; set; }

        public string ConfirmarPassword { get; set; }
    }
}
