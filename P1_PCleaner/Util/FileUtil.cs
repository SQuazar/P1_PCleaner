using System.IO;
using System.Linq;

namespace P1_PCleaner.Util;

public class FileUtil
{
    public static long GetDirectorySize(DirectoryInfo dir)
    {
        return dir.GetFiles().Sum(file => file.Length) + dir.GetDirectories().Sum(GetDirectorySize);
    }
}