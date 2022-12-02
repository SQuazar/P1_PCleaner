using System;
using System.Collections.Generic;
using System.IO;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public class SystemLogFilesScanner : IScanner
{
    public IEnumerable<FileInf> Scan()
    {
        var files = new List<FileInf>();
        DirectoryInfo directory;
        string[] ext;

        // .NET Framework
        ext = new[] { ".log" };
        try
        {
            directory = new DirectoryInfo(@"C:\Windows\Microsoft.NET\Framework\");
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            directory = new DirectoryInfo(@"C:\Windows\Microsoft.NET\Framework64\");
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        // Errors
        try
        {
            directory = new DirectoryInfo(@"C:\ProgramData\Microsoft\Windows\WER\ReportArchive\");
            ext = new[] { ".wer" };
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        // System logs
        DirectoryInfo[] directories;
        ext = new[] { ".log" };
        try
        {
            directories = new[]
            {
                new DirectoryInfo(@"C:\Windows\"),
                new(@"C:\Windows\INF"),
                new(@"C:\Windows\SoftwareDistribution\"),
                new(@"C:\Windows\Microsoft Antimalware\Support\"),
                new(@"C:\Windows\Panther\UnattendGC\"),
                new(@"C:\Windows\Performance\WinSAT\")
            };
            files.AddRange(IScanner.GetFiles(directories, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            directories = new[]
            {
                new DirectoryInfo(@"C:\Windows\debug\"),
                new(@"C:\Windows\Logs\"),
                new(@"C:\Windows\Panther\"),
                new(@"C:\Windows\security\"),
                new(@"C:\Windows\System32\")
            };
            files.AddRange(IScanner.GetFiles(directories, ext, true));
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            directory = new DirectoryInfo(@"C:\Windows\Logs\WindowsUpdate\");
            ext = new[] { ".etl" };
            files.AddRange(IScanner.GetFiles(directory, ext));
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            directory = new DirectoryInfo(@"C:\Windows\SoftwareDistribution\DataStore\Logs");
            ext = new[] { ".log", ".jrs", ".chk" };
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
        return IFilesRepository.ScanCategory.SystemLogFiles;
    }
}