using Q1.Util;
using SharedModules;
using System;
using System.IO;

namespace Q1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            /*
             * Given two sorted files, write a C# program to merge them while preserving sort order.
                The program should determine what data type is used in input files (DateTime, Double, String in that sequence)
                and merge them accordingly.
                DO NOT assume either of these files can fit in memory.

             * */
            Console.WriteLine("Enter first file:");
            string file1 = Console.ReadLine();

            Console.WriteLine("Enter second file:");
            string file2 = Console.ReadLine();


            var fileManager = new LocalFileManager();
            var compareUtil = new CompareUtil();
            using(var sortedFilesMergeUtil = new SortedFilesMergeUtil(fileManager,compareUtil))
            {
                await sortedFilesMergeUtil.MergeAsync(file1, file2);
            }

            Console.WriteLine("Merge complete!!");
            var tempFile = Path.Combine(Path.GetTempPath(), "Merged_File.txt");
            Console.WriteLine($"Verify merge file at {tempFile}");
        }

        
    }
}
