using System.Windows;
using static Blueprint_Inspector.Session;

namespace Blueprint_Inspector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await LoadConfig();
            MainWindow mainWindow = new();
            mainWindow.Show();
            
            Exit += (s, e) => Dispose();
        }
    }
}