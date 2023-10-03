using System.Windows;
using System.Windows.Input;
using Blueprint_Inspector.View;
using Serilog;

namespace Blueprint_Inspector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            ServerProfilesView serverProfilesView = new ();
            BlueprintInspectorView blueprintsView = new ();
            SettingsView settingsView = new ();
            AboutView aboutView = new ();
            LogView logView = new ();
            
            tiServerProfiles.Content = serverProfilesView;
            tiBlueprints.Content = blueprintsView;
            tiSettings.Content = settingsView;
            tiAbout.Content = aboutView;
            tiLogs.Content = logView;
        }

        private void MoveWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Application closed");
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}