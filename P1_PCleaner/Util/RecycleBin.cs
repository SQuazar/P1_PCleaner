using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using P1_PCleaner.Factory;

namespace P1_PCleaner.Util;

public static class RecycleBin
{
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern uint SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pShQueryRbInfo);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags flags);

    public static Info GetInfo()
    {
        var sQueryRbInfo = new SHQUERYRBINFO
        {
            cbSize = Marshal.SizeOf(typeof(SHQUERYRBINFO))
        };
        var result = SHQueryRecycleBin(string.Empty, ref sQueryRbInfo);
        if (result != 0) throw new IOException("Cannot get recycle bin folder info");

        var length = sQueryRbInfo.i64Size;
        var sizeInfo = new FileSizeFactory().CreateInfo(length);

        var info = new Info
        {
            FileCount = sQueryRbInfo.i64NumItems,
            Size = sizeInfo.Size,
            SizeSuffix = sizeInfo.SizeSuffix
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
        Process.Start("explorer.exe", "shell:RecycleBinFolder");
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    // ReSharper disable once InconsistentNaming
    private struct SHQUERYRBINFO
    {
        public int cbSize;
        public readonly long i64Size;
        public readonly long i64NumItems;
    }

    private enum RecycleFlags : uint
    {
        SherBNoConfirmation = 0x00000001,
        SherBNoProgressUi = 0x00000002,
        SherBNoSound = 0x00000004
    }


    public class Info
    {
        public double Size { get; init; }
        public string SizeSuffix { get; set; } = "B";
        public long FileCount { get; init; }
    }
}