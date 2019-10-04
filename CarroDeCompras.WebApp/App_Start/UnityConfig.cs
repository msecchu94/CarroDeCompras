using CarroDeComprasBLL.Implementaciones;
using CarroDeComprasBLL.Interfaces;
using CarroDeComprasDAL.Implementaciones;
using CarroDeComprasDAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp
{
    public static class UnityConfig
    {


        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();


            //container.RegisterType<Login, account>(new InjectionConstructor());
            //container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            ////container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => System.Web.HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IProductoBLL, ProductoBLL>();
            container.RegisterType<IRepositoryProducto, RepositoryProducto>();

            container.RegisterType<IMarcaBLL, MarcaBLL>();
            container.RegisterType<IRepositoryMarca, RepositoryMarca>();

            container.RegisterType<IPedidoBLL, PedidoBLL>();
            container.RegisterType<IRepositoryPedido, RepositoryPedido>();

            container.RegisterType<IUsuarioBLL,UsuarioBLL>();
            container.RegisterType<IRepositoryUsuario,RepositoryUsuario>();

            container.RegisterType<ICarroBLL, CarroBLL>();
            container.RegisterType<IRepositoryCarro, RepositoryCarro>();

            container.RegisterType<IConnectionFactory,ConnectionFactory>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}