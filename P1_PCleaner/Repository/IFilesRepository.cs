using System.Collections.Generic;
using P1_PCleaner.Model;

namespace P1_PCleaner.Repository;

public interface IFilesRepository
{
    Dictionary<ScanCategory, IEnumerable<FileInf>> Files();
    IEnumerable<FileInf> Files(ScanCategory category);
    void Clear();

    public enum ScanCategory
    {
        SystemLogFiles,
        SystemCache,
        TempFiles,
        GoogleChrome,
        MozillaFirefox,
        MicrosoftEdge,
        MicrosoftEdgeLegacy,
        MicrosoftEthernetExplorer,
        Downloads
    }
}