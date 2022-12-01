using System.Threading;
using System.Threading.Tasks;
using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class RecycleBinViewModel : ObservableObject
{
    public RecycleBin.Info Info { get; private set; }

    public RelayCommand OpenRecycleBin => new(_ => RecycleBin.OpenRecycleBinFolder());

    public AsyncRelayCommand ClearRecycleBin => new(_ =>
    {
        RecycleBin.Clear();
        var info = RecycleBin.GetInfo();
        Info = info;
        OnPropertyChanged(nameof(Info));
    });

    public RecycleBinViewModel()
    {
        var info = RecycleBin.GetInfo();
        Info = info;
    }
}