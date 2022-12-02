using System;
using System.Collections.Generic;
using System.IO;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public class SystemCacheFilesScanner : IScanner
{
    public IEnumerable<FileInf> Scan()
    {
        var files = new List<FileInf>();
        DirectoryInfo directory;
        string[] ext;

        // CryptoAPICert Cache
        ext = new[] { "*" };
        try
        {
            directory = new DirectoryInfo(
                $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\LocalLow\Microsoft\CryptnetUrlCache\Content");
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            directory = new DirectoryInfo(
                $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\LocalLow\Microsoft\CryptnetUrlCache\MetaData");
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }
        return files;
    }

    public IFilesRepository.ScanCategory GetScanCategory()
    {
        return IFilesRepository.ScanCategory.SystemCache;
    }
}