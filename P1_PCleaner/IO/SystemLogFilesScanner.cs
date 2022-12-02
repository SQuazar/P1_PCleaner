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
        directory = new DirectoryInfo(@"C:\Windows\Microsoft.NET\Framework\");
        ext = new[] { ".log" };
        files.AddRange(IScanner.GetFiles(directory, ext));
        directory = new DirectoryInfo(@"C:\Windows\Microsoft.NET\Framework64\");
        files.AddRange(IScanner.GetFiles(directory, ext));

        // Errors
        directory = new DirectoryInfo(@"C:\ProgramData\Microsoft\Windows\WER\ReportArchive\");
        ext = new[] { ".wer" };
        files.AddRange(IScanner.GetFiles(directory, ext));

        // System logs
        directory = new DirectoryInfo(@"C:\Windows\");
        ext = new[] { ".log" };
        files.AddRange(IScanner.GetFiles(directory, ext));
        directory = new DirectoryInfo(@"C:\Windows\Logs\WindowsUpdate\");
        ext = new[] { ".etl" };
        files.AddRange(IScanner.GetFiles(directory, ext));
        directory = new DirectoryInfo(@"C:\Windows\SoftwareDistribution\DataStore\Logs");
        ext = new[] { ".log", ".jrs", ".chk" };
        files.AddRange(IScanner.GetFiles(directory, ext));

        return files;
    }

    public IFilesRepository.ScanCategory GetScanCategory()
    {
        return IFilesRepository.ScanCategory.SystemLogFiles;
    }
}