﻿using CarroDeComprasCommon.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasDAL.Interfaces
{
    public interface IRepositoryMarca
    {
        IEnumerable<MarcaBE> CargarMarca();

        MarcaBE ObtenerPorId(int Codigo);

    }
}
