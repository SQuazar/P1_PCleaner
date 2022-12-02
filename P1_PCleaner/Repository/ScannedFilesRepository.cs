using System;
using System.Collections.Generic;
using P1_PCleaner.Factory;
using P1_PCleaner.IO;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.Repository;

public class ScannedFilesRepository : IFilesRepository
{
    private readonly Dictionary<IFilesRepository.ScanCategory, Category> _categories;
    public RecycleBin.Info? RecycleBinInfo { get; private set; } 

    public ScannedFilesRepository()
    {
        _categories = new Dictionary<IFilesRepository.ScanCategory, Category>
        {
            { IFilesRepository.ScanCategory.SystemLogFiles, new Category() },
            { IFilesRepository.ScanCategory.SystemCache, new Category() },
            { IFilesRepository.ScanCategory.TempFiles, new Category() },
            { IFilesRepository.ScanCategory.GoogleChrome, new Category() },
            { IFilesRepository.ScanCategory.MozillaFirefox, new Category() },
            { IFilesRepository.ScanCategory.MicrosoftEdge, new Category() },
            { IFilesRepository.ScanCategory.Downloads, new Category() }
        };
    }

    public Dictionary<IFilesRepository.ScanCategory, Category> Categories()
    {
        return _categories;
    }

    public Category GetCategory(IFilesRepository.ScanCategory category)
    {
        if (!_categories.TryGetValue(category, out var cat))
            throw new ArgumentException($"Cannot find category {category}");
        return cat;
    }

    private static readonly IScanner[] Scanners =
    {
        new SystemLogFilesScanner(),
        new SystemCacheFilesScanner(),
        new SystemTempFilesScanner(),
        new GoggleCacheScanner(),
        new MicrosoftEdgeScanner(),
        new DownloadsScanner()
    };

    public void Load()
    {
        foreach (var scanner in Scanners)
        {
            if (!_categories.TryGetValue(scanner.GetScanCategory(), out var category))
                continue;
            category.Files.AddRange(scanner.Scan());
        }

        RecycleBinInfo = RecycleBin.GetInfo();

    }

    public void Clear()
    {
        _categories.Clear();
        RecycleBinInfo = null;
    }
}