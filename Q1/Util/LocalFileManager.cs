using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Q1.Util
{
    public class LocalFileManager : IFileManager
    {
        public StreamWriter CreateFile(string filePath)
        {
            //TODO:: For now save it under temporary files.
            var tempFile = Path.Combine(Path.GetTempPath(), filePath);
            return new StreamWriter(tempFile);
        }

        public StreamReader ReadAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("File doesn't exist!!");
            }
            return new StreamReader(filePath);
        }
    }
}
