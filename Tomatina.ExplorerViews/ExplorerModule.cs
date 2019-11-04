using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Tomatina.ExplorerViews
{
    public class ExplorerModule : IModule
    {
        
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager= containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(Views.CountryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.CountryView>();
        }
    }
}
