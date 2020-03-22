using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    interface IFileMerge
    {
        Task MergeAsync(string file1Path, string file2Path);
    }
}
