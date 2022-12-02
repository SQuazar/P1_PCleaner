using System.Collections;
using System.Collections.Generic;
using System.Linq;
using P1_PCleaner.Factory;

namespace P1_PCleaner.Model;

public class Category
{
    public List<FileInf> Files { get; } = new();
    public SizeInfo SizeInfo => new FileSizeFactory().CreateInfo(Files.Sum(inf => inf.SizeInfo.Length));
    public bool IsSelected { get; set; }
}