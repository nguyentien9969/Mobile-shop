using Base.DL.DbAccess;
using Base.DL.IRepository;
using Base.DL.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MyShop.Models;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace MyShop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();

            container.RegisterType<IAuthenticationManager>(
            new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            new InjectionConstructor(typeof(ApplicationDbContext)));

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();

            // Kh?i t?o interface; function DBcontext
            // 
            container.RegisterType<IApplicationDbContext, MainContext>(); // KH?i t?o DBcontex 
            container.RegisterType<IUnitOfWork, UnitOfWork>(); // Dùng DB context ?? l?y d? li?u các b?ng trong database

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}