using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharedModules
{
    public interface IFileManager
    {
        StreamReader Read(string filePath);

        StreamWriter CreateFile(string filePath);

        Type DiscoverTypeOfData(string file1, string file2);
        Type DiscoverTypeOfData(string data);
    }
}
