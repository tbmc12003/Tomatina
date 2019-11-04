using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Events;
using Prism.Events;
using Unity;

namespace Tomatina.WinApp.ViewModels
{
    public class MainViewModel: BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _isActive;

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        [InjectionMethod]
        public void Init()
        {
            _eventAggregator.GetEvent<BusyEvent>().Subscribe((state) =>
            {
                IsActive = state;
            });
            Opacity = IsActive ? 0.5 : 1;
        }


        
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                Opacity = _isActive ? 0.5 : 1;
                RaisePropertyChanged(nameof(IsActive));
            }
        }

        private double _opacity;
        public double Opacity
        {
            get => _opacity;
            set
            {
                _opacity = value;
                RaisePropertyChanged(nameof(Opacity));
            }
        }
    }

    
}
