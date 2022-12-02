using P1_PCleaner.Factory;

namespace P1_PCleaner.Model;

public class SizeInfo
{
    public double Size { get; set; }
    public long Length { get; set; }
    public string SizeSuffix { get; set; } = "B";
    
    public static SizeInfo operator +(SizeInfo src, SizeInfo dest)
    {
        return new FileSizeFactory().CreateInfo(src.Length + dest.Length);
    }
}