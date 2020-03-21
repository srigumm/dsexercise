using Q1.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Q1
{
    public class SortedFilesMergeUtil : IFileMerge,IDisposable
    {
        private readonly IFileManager fileManager;
        private readonly ICompareUtil compareUtil;
        const string mergedFileNameFormat = "Merged_File.txt";

        public SortedFilesMergeUtil(IFileManager fileManager, ICompareUtil compareUtil)
        {
            this.fileManager = fileManager;
            this.compareUtil = compareUtil;
        }
        public void Merge(string file1, string file2)
        {
            string rowFile1 = string.Empty;
            string rowFile2 = string.Empty;

            bool advanceFile1 = true;
            bool advanceFile2 = true;


            using (var file1Reader = fileManager.ReadAsync(file1)) //stream file1 content
            {
                using (var file2Reader = fileManager.ReadAsync(file2)) //stream file2 content
                {
                    Type typeOfDateInFiles = fileManager.DiscoverTypeOfData(file1, file2);
                    Func<string, string, bool> compareFunc = this.compareUtil.getComparer(typeOfDateInFiles);

                    _mergeFileStream = fileManager.CreateFile(mergedFileNameFormat);
                    {
                        while( (!file1Reader.EndOfStream || (advanceFile2 && file1Reader.EndOfStream ) ) && 
                            (!file2Reader.EndOfStream || (advanceFile1 && file2Reader.EndOfStream)))
                        {
                            if (advanceFile1)
                            {
                                rowFile1 = file1Reader.ReadLine();
                                
                            }
                            if (advanceFile2) { 
                                rowFile2 = file2Reader.ReadLine();
                            }

                            if (!string.IsNullOrEmpty(rowFile1))
                            {
                                rowFile1 = rowFile1.Trim();
                            }
                            if(!string.IsNullOrEmpty(rowFile2))
                            {
                                rowFile2 = rowFile2.Trim();
                            }
                            //TODO
                            //if((string.IsNullOrEmpty(rowFile1) || rowFile2 == Environment.NewLine) && (string.IsNullOrEmpty(rowFile2) || rowFile2==Environment.NewLine)) //when both are empty, advance both pointers
                            //{
                            //    advanceFile1 = true;
                            //    advanceFile2 = true;
                            //}
                            if (compareFunc(rowFile1, rowFile2))
                            {
                                Console.WriteLine(rowFile1);
                                if (!string.IsNullOrEmpty(rowFile1))
                                {
                                    _mergeFileStream.WriteLine(rowFile1);
                                }
                                advanceFile1 = true;
                                advanceFile2 = false;
                            }
                            else
                            {
                                Console.WriteLine(rowFile2);
                                if (!string.IsNullOrEmpty(rowFile2))
                                {
                                    _mergeFileStream.WriteLine(rowFile2);
                                }
                                advanceFile2 = true;
                                advanceFile1 = false;
                            }
                        }
                        if (advanceFile1 && file1Reader.EndOfStream)
                        {
                            if (!string.IsNullOrEmpty(rowFile2))
                            {
                                _mergeFileStream.WriteLine(rowFile2);
                            }
                            while (!file2Reader.EndOfStream)
                            {
                                _mergeFileStream.WriteLine(file2Reader.ReadLine());
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(rowFile2))
                            {
                                _mergeFileStream.WriteLine(rowFile1);
                            }
                            while (!file1Reader.EndOfStream)
                            {
                                _mergeFileStream.WriteLine(file1Reader.ReadLine());
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            using (_mergeFileStream) { }
        }

        private Func<string, string, bool> IsInOrder = (s1, s2) =>
          {
              if(!string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2))
              {
                  //Try int
                  int i = -1;
                  int j = -1;
                  if(Int32.TryParse(s1,out i) && Int32.TryParse(s2, out j))
                  {
                      return i <= j;
                  }
              }
              return true;
          };
        private StreamWriter _mergeFileStream;
    }
}
