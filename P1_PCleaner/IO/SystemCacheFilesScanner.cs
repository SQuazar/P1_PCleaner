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
        directory = new DirectoryInfo(
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\LocalLow\Microsoft\CryptnetUrlCache\Content");
        ext = new[] { "*" };
        files.AddRange(IScanner.GetFiles(directory, ext));
        directory = new DirectoryInfo(
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\LocalLow\Microsoft\CryptnetUrlCache\MetaData");
        files.AddRange(IScanner.GetFiles(directory, ext));
        return files;
    }

    public IFilesRepository.ScanCategory GetScanCategory()
    {
        return IFilesRepository.ScanCategory.SystemCache;
    }
}