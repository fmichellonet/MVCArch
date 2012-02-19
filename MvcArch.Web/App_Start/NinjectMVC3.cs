using System;
using System.Data.Entity;
using EntityFramework.Patterns;
using Ninject.Extensions.Conventions;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MvcArch.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(MvcArch.Web.App_Start.NinjectMVC3), "Stop")]

namespace MvcArch.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind(typeof(IUnitOfWork)).To(typeof(UnitOfWork));

            kernel.Bind<DbContextAdapter>().ToSelf().InRequestScope();
            kernel.Bind<IObjectSetFactory>().ToMethod(x => x.Kernel.Get<DbContextAdapter>()).InRequestScope();
            kernel.Bind<IObjectContext>().ToMethod(x => x.Kernel.Get<DbContextAdapter>()).InRequestScope();

            kernel.Scan(scanner =>
                            {
                                scanner.From("MvcArch.IServices");
                                scanner.From("MvcArch.Services");
                                scanner.BindWithDefaultConventions();
                            }
                        );

            Type contextType = Type.GetType("MvcArch.Dal.Context, MvcArch.Dal");
            kernel.Bind(typeof(DbContext)).To(contextType);
        }        
    }
}
