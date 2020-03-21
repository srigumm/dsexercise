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
        const string mergedFileNameFormat = "Merged_File.txt"; //TODO:: Can be Merge_<<File1>>_<<File2>>.txt

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


            using (var file1Reader = fileManager.ReadAsync(file1)) //stream file1 content without reading entire file into memory
            {
                using (var file2Reader = fileManager.ReadAsync(file2)) //stream file2 content without reading entire file into memory
                {
                    //Discover data type of input files.
                    Type typeOfDateInFiles = fileManager.DiscoverTypeOfData(file1, file2);

                    Func<string, string, bool> IsInOrderFunc = this.compareUtil.getComparer(typeOfDateInFiles);

                    _mergeFileStream = fileManager.CreateFile(mergedFileNameFormat);

                    while( (!file1Reader.EndOfStream || (advanceFile2 && file1Reader.EndOfStream)) &&
                           (!file2Reader.EndOfStream || (advanceFile1 && file2Reader.EndOfStream))) 
                    {
                            if (advanceFile1)
                            {
                                rowFile1 = file1Reader.ReadLine();
                                
                            }
                            if (advanceFile2) { 
                                rowFile2 = file2Reader.ReadLine();
                            }

                            //TODO
                            //if((string.IsNullOrEmpty(rowFile1) || rowFile2 == Environment.NewLine) && (string.IsNullOrEmpty(rowFile2) || rowFile2==Environment.NewLine)) //when both are empty, advance both pointers
                            //{
                            //    advanceFile1 = true;
                            //    advanceFile2 = true;
                            //}
                            if (IsInOrderFunc(rowFile1, rowFile2))
                            {
                                Console.WriteLine(rowFile1);
                                if (!string.IsNullOrEmpty(rowFile1))
                                {
                                    _mergeFileStream.WriteLine(rowFile1.Trim());
                                }
                                advanceFile1 = true;
                                advanceFile2 = false;
                            }
                            else
                            {
                                Console.WriteLine(rowFile2);
                                if (!string.IsNullOrEmpty(rowFile2))
                                {
                                    _mergeFileStream.WriteLine(rowFile2.Trim());
                                }
                                advanceFile2 = true;
                                advanceFile1 = false;
                            }
                        }
                        if (advanceFile1 && file1Reader.EndOfStream) //reached end of file1, write all rows in file2 directly
                        {
                            if (!string.IsNullOrEmpty(rowFile2))
                            {
                                _mergeFileStream.WriteLine(rowFile2.Trim());
                            }
                            while (!file2Reader.EndOfStream)
                            {
                                rowFile2 = file2Reader.ReadLine();
                                if (!string.IsNullOrEmpty(rowFile2))
                                {
                                    _mergeFileStream.WriteLine(rowFile2.Trim());
                                }
                            }
                        }
                        else //reached end of file2, write all rows in file1 directly
                        {
                                if (!string.IsNullOrEmpty(rowFile1))
                                {
                                    _mergeFileStream.WriteLine(rowFile1);
                                }
                                while (!file1Reader.EndOfStream)
                                {
                                    rowFile1 = file1Reader.ReadLine();
                                    if (!string.IsNullOrEmpty(rowFile1))
                                    {
                                        _mergeFileStream.WriteLine(rowFile1.Trim());
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
        private StreamWriter _mergeFileStream;
    }
}
