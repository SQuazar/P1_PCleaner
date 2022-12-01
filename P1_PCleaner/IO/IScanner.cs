using System.Collections.Generic;
using P1_PCleaner.Model;

namespace P1_PCleaner.IO;

public interface IScanner
{
    List<FileInf> Scan();
}