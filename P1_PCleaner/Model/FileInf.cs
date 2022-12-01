using System;
using System.IO;
using P1_PCleaner.Util;

namespace P1_PCleaner.Model;

public class FileInf
{
    public string Path { get; set; } = null!;
    public string Name => System.IO.Path.GetFileName(Path);
    public DateTime CreationTime => File.GetCreationTime(Path).Date;

    public SizeInfo Size
    {
        get
        {
            double size;
            size = !IsDirectory
                ? File.Open(Path, FileMode.Open).Length
                : FileUtil.GetDirectorySize(new DirectoryInfo(Path));
            string sizeSuffix;
            if (size < 1024)
                sizeSuffix = "B";
            else if (size < Math.Pow(1024, 2))
                sizeSuffix = "KB";
            else if (size < Math.Pow(1024, 3))
                sizeSuffix = "MB";
            else if (size < Math.Pow(1024, 4))
                sizeSuffix = "GB";
            else if (size < Math.Pow(1024, 5))
                sizeSuffix = "TB";
            else sizeSuffix = "unresolved";
            size = sizeSuffix switch
            {
                "B" => size,
                "KB" => size / 1024,
                "MB" => size / 1024 / 1024,
                "GB" => size / 1024 / 1024 / 1024,
                "TB" => size / 1024 / 1024 / 1024 / 1024,
                _ => 0
            };
            return new SizeInfo { Size = size, SizeSuffix = sizeSuffix };
        }
    }

    public bool IsDirectory => File.GetAttributes(Path).HasFlag(FileAttributes.Directory);

    public class SizeInfo
    {
        public double Size { get; set; }
        public string SizeSuffix { get; set; } = "B";
    }
}