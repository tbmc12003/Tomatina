using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using Core.Core;
using Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Tomatina.ExplorerViews.Data;
using Unity;

namespace Tomatina.ExplorerViews.ViewModels
{
    public class CountryViewModel:BindableBase
    {
        private ObservableCollection<Country> _countries;
        public IEventAggregator EventAggregator { get; }

        public CountryViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            LoadCommand = new DelegateCommand(async ()=>await OnLoadCommand());
        }

      

        [InjectionMethod]
        public async void Init()
        {
            
        }

        public  DelegateCommand LoadCommand { get; }

        private async Task OnLoadCommand()
        {
            EventAggregator.GetEvent<BusyEvent>().Publish(true);
            await Task.Run(() =>
            {
                var resourceFile =
                    Path.Combine(Assembly.GetExecutingAssembly().GetAssemblyLocation(), "Resources", "countries.json");
                var countriesJson = resourceFile.ReadFromFile();

                var countryList = countriesJson.Deserialize<List<Country>>();
                countryList.Shuffle();

                Countries = new ObservableCollection<Country>(countryList);

                return true;
            }).ContinueWith(tResult =>
            {
                Thread.Sleep(10000);
                EventAggregator.GetEvent<BusyEvent>().Publish(false);
            });
        }

        public ObservableCollection<Country> Countries
        {
            get => _countries;
            set
            {
                _countries = value;
                RaisePropertyChanged();
            }
        }
    }
}