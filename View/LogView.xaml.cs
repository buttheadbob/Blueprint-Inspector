using System.Windows.Controls;

namespace Blueprint_Inspector.View;

public partial class LogView : UserControl
{
    public LogView()
    {
        InitializeComponent();
        Session.InitLogger(rtbLoggerView);
    }
}