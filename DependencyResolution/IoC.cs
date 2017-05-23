using BizCover.Address.Search.Business;
using BizCover.Address.Search.BusinessInterfaces;
using Ninject;

namespace BizCover.Address.Search.DependencyResolution
{
    public class IoC
    {
        public static void ConfigureIoC(IKernel kernel)
        {
            kernel.Bind<IBCAddressSearchService>().To<BCAddressSearchService>().InSingletonScope();
        }
    }
}
