﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string IdRol { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }

        public string PasswordSalt { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public RolesDTO Roles { get; set; }

    }
}
