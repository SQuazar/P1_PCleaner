using System.Collections.Generic;
using P1_PCleaner.Model;

namespace P1_PCleaner.Repository;

public interface IFilesRepository
{
    public enum ScanCategory
    {
        SystemLogFiles,
        SystemCache,
        TempFiles,
        GoogleChrome,
        MozillaFirefox,
        MicrosoftEdge,
        Downloads
    }

    Dictionary<ScanCategory, Category> Categories();
    Category GetCategory(ScanCategory category);
    void Load();
    void Clear();
}