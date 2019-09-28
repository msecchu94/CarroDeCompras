using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarroDeComprasCommon.Entidad
{
    public class ProductoBE
    {
     public int Codigo{get;set;}

     public string Nombre{get;set;}

     public string Descripcion{get;set;}

     public int IdMarca{get;set;}

     public decimal PrecioUnitario{get;set;}

     public bool Activo{get;set;}

     public string UrlImange{get;set;}

     public MarcaBE Marca { get; set; }

    }
}