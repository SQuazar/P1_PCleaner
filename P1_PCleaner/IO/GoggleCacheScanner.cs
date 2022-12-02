using System;
using System.Collections.Generic;
using System.IO;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public class GoggleCacheScanner : IScanner
{
    public IEnumerable<FileInf> Scan()
    {
        var files = new List<FileInf>();
        DirectoryInfo directory;
        string[] ext;

        // Google Cache Storage
        try
        {
            directory = new DirectoryInfo(
                $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Google\Chrome\User Data\Default\Service Worker\CacheStorage");
            ext = new[] { "*" };
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            directory = new DirectoryInfo(@"C:\Program Files\Google\Chrome\Application");
            ext = new[] { ".7z" };
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
        return IFilesRepository.ScanCategory.GoogleChrome;
    }
}