﻿using System;
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

        public Type DiscoverTypeOfData(string file1, string file2)
        {
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
            else
            {
                return typeof(string);
            }
        }
    }
}
