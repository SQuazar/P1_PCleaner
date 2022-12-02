using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class RecycleBinViewModel : ObservableObject
{
    public RecycleBinViewModel()
    {
        var info = App.ScannedRepository.RecycleBinInfo ?? new RecycleBin.Info();
        Info = info;
    }

    public RecycleBin.Info Info { get; private set; }

    public RelayCommand OpenRecycleBin => new(_ => RecycleBin.OpenRecycleBinFolder());

    public AsyncRelayCommand ClearRecycleBin => new(_ =>
    {
        RecycleBin.Clear();
        var info = RecycleBin.GetInfo();
        Info = info;
        OnPropertyChanged(nameof(Info));
    });
}