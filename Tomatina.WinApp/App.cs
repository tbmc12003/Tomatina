using Prism.Unity;
using System.Windows;
using Prism.Ioc;
using Prism.Modularity;
using Tomatina.ExplorerViews;

namespace Tomatina.WinApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ExplorerModule>();

            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
