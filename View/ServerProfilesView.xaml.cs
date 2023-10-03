using System.Linq;
using System.Windows.Controls;
using Blueprint_Inspector.Models;
using Blueprint_Inspector.Utils;

namespace Blueprint_Inspector.View;

public partial class ServerProfilesView : UserControl
{
    BetterObservableCollection<Block> ListOfBlocks { get; set; } = new();
    BetterObservableCollection<string> ListOfComponents { get; set; } = new();
    public ServerProfilesView()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void CboServerBlockList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cboServerProfileList.SelectedItem is string selectedServer)
        {
            // Update List with blocks of the selected server
            ListOfBlocks.Clear();
            ListOfBlocks.AddRange(Session.ServerProfileList.First(serverProfile => serverProfile.ServerName == selectedServer).Blocks.ToList());
        }
    }

    private void BlocksListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BlocksListView.SelectedItem is Block selectedBlock)
        {
            // Update List with components of the selected block
            ListOfComponents.Clear();
            ListOfComponents.AddRange(selectedBlock.ComponentsList.ToList());
        }
    }
}