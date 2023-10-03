namespace Blueprint_Inspector.Settings;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Config : INotifyPropertyChanged
{
    private string _blueprintFolder;
    private string _serverProfileFolder;

    public string BlueprintFolder
    {
        get => _blueprintFolder;
        set
        {
            if (_blueprintFolder != value)
            {
                _blueprintFolder = value;
                OnPropertyChanged();
            }
        }
    }

    public string ServerProfileFolder
    {
        get => _serverProfileFolder;
        set
        {
            if (_serverProfileFolder != value)
            {
                _serverProfileFolder = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
