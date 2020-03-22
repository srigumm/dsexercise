using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharedModules
{
    public class LocalFileManager : IFileManager
    {
        public StreamWriter CreateFile(string filePath)
        {
            //TODO:: For now save it under temporary files.
            var tempFile = Path.Combine(Path.GetTempPath(), filePath);
            return new StreamWriter(tempFile);
        }

        public Type DiscoverTypeOfData(string file1, string file2)
        {
            //TODO:: For now, look at first file content and identify the type of data, but this logic can be modified to consider content of both files.
            using(var sr = new StreamReader(file1))
            {
                string firstLine = sr.ReadLine(); //First Line.

                return DiscoverTypeOfData(firstLine);
                
            }
        }

        private bool isInteger(string firstLine)
        {
            int i = -1;
            return Int32.TryParse(firstLine, out i);
        }
        private bool isDouble(string data)
        {
            double i = -1;
            return Double.TryParse(data, out i);
        }

        private bool isDateTime(string firstLine)
        {
            DateTime dateTime = DateTime.MinValue;
            return DateTime.TryParse(firstLine, out dateTime);
        }

        public StreamReader ReadAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("File doesn't exist!!");
            }
            return new StreamReader(filePath);
        }

        public Type DiscoverTypeOfData(string data)
        {
            if (isDateTime(data))
            {
                return typeof(DateTime);
            }
            else if (isInteger(data))
            {
                return typeof(Int32);
            }
            else if (isDouble(data))
            {
                return typeof(Double);
            }
            else
            {
                return typeof(string);
            }
        }

       
    }
}
