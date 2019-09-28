﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasCommon.DTO
{
    public class ProductoDTO
    {
        public int Codigo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int IdMarca { get; set; }

        public decimal PrecioUnitario { get; set; }

        public bool Activo { get; set; }

        public string UrlImange { get; set; }

        public MarcaDTO Marca { get; set; }

    }
}
