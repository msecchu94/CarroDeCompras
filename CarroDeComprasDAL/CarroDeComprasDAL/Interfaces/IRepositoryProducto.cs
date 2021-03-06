﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using CarroDeComprasCommon.Entidad;


namespace CarroDeComprasDAL.Interfaces
{
    public interface IRepositoryProducto
    {

        IEnumerable<ProductoBE> ObtenerProductos();

        IEnumerable<ProductoBE> ObtenerProductosActivos();

        bool AltaProducto(ProductoBE productoBE);

        ProductoBE ObtenerPorCodigo(int códigoProducto);

        bool EditarProducto(ProductoBE productoBE);


    }
}
