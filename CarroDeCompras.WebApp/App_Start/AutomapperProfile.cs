using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasCommon.DTO;
using WebApp.Models;

namespace WebApp.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(config =>

            {
                config.CreateMap<MarcaBE, MarcaDTO>().ForMember(x=>x.Nombre,y=>y.MapFrom(z=>z.Nombre));
                config.CreateMap<ProductoBE, ProductoDTO>().ForMember(X => X.Marca, y => y.MapFrom(z => z.Marca));
                config.CreateMap<DetallePedidoBE, DetallePedidoDTO>().ForMember(x => x.ProductoDTO, y => y.MapFrom(z => z.ProductoBE));
                config.CreateMap<PedidoBE, PedidoDTO>();
                

                config.CreateMap<MarcaDTO,Marca>();
                config.CreateMap<ProductoDTO,Producto>();
                config.CreateMap<DetallePedidoDTO,DetallePedidoModel>().ForMember(x => x.Producto, y => y.MapFrom(z => z.ProductoDTO));
                config.CreateMap<PedidoDTO,PedidoModel>();
                config.CreateMap<UsuarioDTO,UsuarioModel>();

                config.CreateMap<MarcaDTO, MarcaBE>();
                config.CreateMap<ProductoDTO, ProductoBE>();
                config.CreateMap<DetallePedidoDTO, DetallePedidoBE>().ForMember(x => x.ProductoBE, y => y.MapFrom(z => z.ProductoDTO));
                config.CreateMap<PedidoDTO, PedidoBE>();

                config.CreateMap<Marca,MarcaDTO>();
                config.CreateMap<Producto,ProductoDTO>();
                config.CreateMap<DetallePedidoModel,DetallePedidoDTO>().ForMember(x => x.ProductoDTO, y => y.MapFrom(z => z.Producto));
                config.CreateMap<PedidoModel,PedidoDTO>();
                config.CreateMap<UsuarioModel,UsuarioDTO>();


                config.CreateMap<Producto,ProductoDTO>();
                config.CreateMap<DetallePedidoModel,DetallePedidoDTO>().ForMember(x => x.ProductoDTO, y => y.MapFrom(z => z.Producto));
                config.CreateMap<PedidoModel,PedidoDTO>();

            });
        }
    }
}