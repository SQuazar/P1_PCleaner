using P1_PCleaner.Model;

namespace P1_PCleaner.Factory;

public interface IFileSizeFactory
{
    SizeInfo CreateInfo(long length);
}