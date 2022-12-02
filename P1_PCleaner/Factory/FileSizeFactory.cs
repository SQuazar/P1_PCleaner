using System;
using P1_PCleaner.Model;

namespace P1_PCleaner.Factory;

public class FileSizeFactory : IFileSizeFactory
{
    public SizeInfo CreateInfo(long length)
    {
        double size = length;
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
        return new SizeInfo { Size = size, Length = length, SizeSuffix = sizeSuffix };
    }
}