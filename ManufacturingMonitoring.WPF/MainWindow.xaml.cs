using System.Windows;
using ManufacturingMonitoring.WPF.ViewModels;

namespace ManufacturingMonitoring.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                await vm.RefreshMetricsAsync();
            }
        }
    }
}
