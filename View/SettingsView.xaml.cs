using System.Windows.Controls;

namespace Blueprint_Inspector.View;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = Session.Config;
    }
}