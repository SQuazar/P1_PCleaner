using System;
using System.Collections.Generic;
using P1_PCleaner.IO;
using P1_PCleaner.Model;
using P1_PCleaner.Util;

namespace P1_PCleaner.Repository;

public class ScannedFilesRepository : IFilesRepository
{
    private static readonly IScanner[] Scanners =
    {
        new SystemLogFilesScanner(),
        new SystemCacheFilesScanner(),
        new SystemTempFilesScanner(),
        new GoggleCacheScanner(),
        new MicrosoftEdgeScanner(),
        new DownloadsScanner()
    };

    private readonly Dictionary<IFilesRepository.ScanCategory, Category> _categories;

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

    public RecycleBin.Info? RecycleBinInfo { get; set; }

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

    public event Action<IFilesRepository.ScanCategory>? CategoryStartLoading; 
    public event Action<IFilesRepository.ScanCategory, Category>? CategoryLoaded;

    public void Load()
    {
        foreach (var scanner in Scanners)
        {
            if (!_categories.TryGetValue(scanner.GetScanCategory(), out var category))
                continue;
            CategoryStartLoading?.Invoke(scanner.GetScanCategory());
            var files = scanner.Scan();
            category.Files.AddRange(files);
            CategoryLoaded?.Invoke(scanner.GetScanCategory(), category);
        }

        RecycleBinInfo = RecycleBin.GetInfo();
    }

    public void Clear()
    {
        foreach (var key in _categories.Keys)
            _categories[key] = new Category();
        RecycleBinInfo = null;
    }
}