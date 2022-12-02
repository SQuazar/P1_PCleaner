using System;
using System.IO;

namespace P1_PCleaner.Model;

public class FileInf
{
    public string Path { get; set; } = null!;
    public string Name => System.IO.Path.GetFileName(Path);
    public DateTime CreationTime => File.GetCreationTime(Path).Date;

    public SizeInfo SizeInfo { get; set; } = new();

    public bool IsDirectory
    {
        get
        {
            try
            {
                return File.GetAttributes(Path).HasFlag(FileAttributes.Directory);
            }
            catch (UnauthorizedAccessException e)
            {
                return false;
            }
        }
    }
}