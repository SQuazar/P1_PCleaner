using System;
using System.Collections.Generic;
using System.IO;
using P1_PCleaner.Factory;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public class MicrosoftEdgeScanner : IScanner
{
    public IEnumerable<FileInf> Scan()
    {
        var files = new List<FileInf>();
        DirectoryInfo directory;
        string[] ext;

        // Cache Storage
        try
        {
            directory = new DirectoryInfo(
                $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Microsoft\Edge\User Data\Default\Service Worker\CacheStorage");
            ext = new[] { "*" };
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        // History
        var factory = new FileSizeFactory();
        try
        {
            var history =
                new FileInfo(
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Microsoft\Edge\User Data\Default\History");
            files.Add(new FileInf { Path = history.FullName, SizeInfo = factory.CreateInfo(history.Length) });
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            var visits =
                new FileInfo(
                    $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Microsoft\Edge\User Data\Default\Visited Links");
            files.Add(new FileInf { Path = visits.FullName, SizeInfo = factory.CreateInfo(visits.Length) });
        }
        catch (Exception)
        {
            // ignored
        }

        return files;
    }

    public IFilesRepository.ScanCategory GetScanCategory()
    {
        return IFilesRepository.ScanCategory.MicrosoftEdge;
    }
}