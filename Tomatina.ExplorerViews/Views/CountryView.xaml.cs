using System.Windows.Controls;

namespace Tomatina.ExplorerViews.Views
{
    /// <summary>
    /// Interaction logic for CountryView.xaml
    /// </summary>
    public partial class CountryView : UserControl
    {
        public CountryView(ViewModels.CountryViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
