using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Q1.Util
{
    public interface IFileManager
    {
        StreamReader ReadAsync(string filePath);

        StreamWriter CreateFile(string filePath);
    }
}
