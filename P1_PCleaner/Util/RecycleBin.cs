using System;
using System.IO;
using System.Runtime.InteropServices;

namespace P1_PCleaner.Util;

public static class RecycleBin
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    // ReSharper disable once InconsistentNaming
    private struct SHQUERYRBINFO
    {
        public int cbSize;
        public long i64Size;
        public long i64NumItems;
    }

    private enum RecycleFlags : uint
    {
        SherBNoConfirmation = 0x00000001,
        SherBNoProgressUi = 0x00000002,
        SherBNoSound = 0x00000004
    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern uint SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pShQueryRbInfo);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags flags);


    public struct Info
    {
        public double Size { get; init; }
        public string SizeSuffix { get; set; }
        public long FileCount { get; init; }
    }

    public static Info GetInfo()
    {
        var sQueryRbInfo = new SHQUERYRBINFO
        {
            cbSize = Marshal.SizeOf(typeof(SHQUERYRBINFO))
        };
        var result = SHQueryRecycleBin(string.Empty, ref sQueryRbInfo);
        if (result != 0) throw new IOException("Cannot get recycle bin folder info");

        var size = (double)sQueryRbInfo.i64Size;
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

        var info = new Info
        {
            FileCount = sQueryRbInfo.i64NumItems,
            Size = size,
            SizeSuffix = sizeSuffix
        };
        return info;
    }

    public static bool Clear()
    {
        var result = SHEmptyRecycleBin(IntPtr.Zero, string.Empty, 0);
        return result == 0;
    }

    public static void OpenRecycleBinFolder()
    {
        System.Diagnostics.Process.Start("explorer.exe", "shell:RecycleBinFolder");
    }
}