using System;
using System.Collections.Generic;
using System.IO;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public class DownloadsScanner : IScanner
{
    public IEnumerable<FileInf> Scan()
    {
        return IScanner.GetFiles(
            new DirectoryInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Downloads"),
            new[] { "*" });
    }

    public IFilesRepository.ScanCategory GetScanCategory()
    {
        return IFilesRepository.ScanCategory.Downloads;
    }
}