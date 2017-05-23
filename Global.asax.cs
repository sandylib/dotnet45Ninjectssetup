using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BizCover.Address.Search.Api
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected override void OnApplicationStarted()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            base.OnApplicationStarted();
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new NinjectSettings() { InjectNonPublic = true });
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            DependencyResolution.IoC.ConfigureIoC(kernel);
        }
    }
}
