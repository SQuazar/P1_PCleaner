using P1_PCleaner.Command;
using P1_PCleaner.Util;

namespace P1_PCleaner.ViewModel;

public class RecycleBinViewModel : ObservableObject
{
    public double TotalGb { get; }
    public long FilesCount { get; }

    public RelayCommand OpenRecycleBin => new(_ => RecycleBin.OpenRecycleBinFolder());

    public RecycleBinViewModel()
    {
        var info = RecycleBin.GetInfo();
        TotalGb = info.GSize;
        FilesCount = info.FileCount;

    }
}