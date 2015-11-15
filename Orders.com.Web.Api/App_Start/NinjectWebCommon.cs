[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Orders.com.Web.Api.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Orders.com.Web.Api.NinjectWebCommon), "Stop")]

namespace Orders.com.Web.Api
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System.Configuration;
    using Configuration;
    using BLL;
    using DAL.InMemory;
    using DataProxy;
    using Peasy;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
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
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            DIConfigSection configSection = (DIConfigSection)ConfigurationManager.GetSection("DIConfigurationSection");

            foreach (var binding in configSection.Bindings)
            {
                try
                {
                    var ninjectBinding = kernel.Bind(Type.GetType(binding.FromType)).To(Type.GetType(binding.ToType));
                    if (binding.AsSingleton) ninjectBinding.InSingletonScope();
                    foreach (var defaultProp in binding.DefaultProperties)
                        ninjectBinding.WithPropertyValue(defaultProp.PropertyName, Cast(defaultProp.Value, defaultProp.Type));
                }
                catch
                {
                    throw new Exception(string.Format("Couldn't resolve {0} to {1}", binding.FromType, binding.ToType));
                }
            }

            //CreateBindingConfigString(typeof(IOrderItemService), typeof(OrderItemService));
        }

        private static object Cast(string value, string type)
        {
            var returnVal = Convert.ChangeType(value, Type.GetType(type));
            return returnVal;
        }

        private static void CreateBindingConfigString(Type fromType, Type toType)
        {
            var from = string.Format("{0}, {1}", fromType.FullName, fromType.Assembly.ToString().Split(',')[0]);
            var to = string.Format("{0}, {1}", toType.FullName, toType.Assembly.ToString().Split(',')[0]);
            System.Diagnostics.Debug.WriteLine(string.Format("<add fromType=\"{0}\" toType=\"{1}\" />", from, to));
        }   

    }
}
