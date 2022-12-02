using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using P1_PCleaner.Factory;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public interface IScanner
{
    IEnumerable<FileInf> Scan();

    IFilesRepository.ScanCategory GetScanCategory();

    static IEnumerable<FileInf> GetFiles(DirectoryInfo directory, string[] ext, bool deep = true)
    {
        if (directory == null) throw new ArgumentNullException(nameof(directory));
        IFileSizeFactory factory = new FileSizeFactory();
        return directory.EnumerateFiles("*.*", new EnumerationOptions
                { RecurseSubdirectories = true, MatchType = MatchType.Win32, AttributesToSkip = 0 })
            .Where(fileInfo => ext.Any(x => fileInfo.Extension.EndsWith(x) || x.Equals("*")))
            .Select(fileInfo => new FileInf
                { Path = fileInfo.FullName, SizeInfo = factory.CreateInfo(fileInfo.Length) });
    }
}