using System;
using System.Collections.Generic;
using P1_PCleaner.Model;

namespace P1_PCleaner.Repository;

public class ScannedFilesRepository : IFilesRepository
{
    private readonly Dictionary<IFilesRepository.ScanCategory, IEnumerable<FileInf>> _files;

    public ScannedFilesRepository()
    {
        _files = new Dictionary<IFilesRepository.ScanCategory, IEnumerable<FileInf>>
        {
            { IFilesRepository.ScanCategory.SystemLogFiles, new List<FileInf>() },
            { IFilesRepository.ScanCategory.SystemCache, new List<FileInf>() },
            { IFilesRepository.ScanCategory.TempFiles, new List<FileInf>() },
            { IFilesRepository.ScanCategory.GoogleChrome, new List<FileInf>() },
            { IFilesRepository.ScanCategory.MozillaFirefox, new List<FileInf>() },
            { IFilesRepository.ScanCategory.MicrosoftEdge, new List<FileInf>() },
            { IFilesRepository.ScanCategory.MicrosoftEdgeLegacy, new List<FileInf>() },
            { IFilesRepository.ScanCategory.MicrosoftEthernetExplorer, new List<FileInf>() },
            { IFilesRepository.ScanCategory.Downloads, new List<FileInf>() }
        };
    }

    public Dictionary<IFilesRepository.ScanCategory, IEnumerable<FileInf>> Files()
    {
        return _files;
    }

    public IEnumerable<FileInf> Files(IFilesRepository.ScanCategory category)
    {
        IEnumerable<FileInf> fileInfs;
        if (!_files.TryGetValue(category, out fileInfs!))
            throw new ArgumentException($"Cannot find files in selected category {category}");
        return fileInfs;
    }

    public void Clear()
    {
        _files.Clear();
    }
}