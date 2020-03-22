using Q2.DataPipeline.Pipeline;
using SharedModules;
using System;
using System.Diagnostics;

namespace Q2.DataPipeline
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            /*
             Problem 1
            You are given a file formatted like this:
            CUSIP
            Price
            Price
            Price
            …
            CUSIP
            Price
            Price
            CUSIP
            Price
            Price
            Price
            …
            Price
            CUSIP
            Price
            …

            Think of it as a file of price ticks for a set of bonds identified by their CUSIPs.
            You can assume a CUSIP is just an 8-character alphanumeric string. Each CUSIP may have any
            number of prices (e.g., 95.752, 101.255) following it in sequence, one per line.
            The prices can be considered to be ordered by time in ascending order, earliest to latest.
            Write a C# program that will print the opening, lowest, highest, closing price for each CUSIP in
            the file.
            DO NOT assume the entire file can fit in memory. 
            */
            Console.WriteLine("Enter input file:");
            string file1 = Console.ReadLine();

            //var sw = Stopwatch.StartNew();
            var fileManager = new LocalFileManager();
            var statsCalculator = new CusipStatsCalculator();
            var cusipFileUtil = new CusipDataPipeline(fileManager, statsCalculator);
            await cusipFileUtil.RunAsync(file1);

            //sw.Stop();
            //Console.WriteLine($"Time to process blocks: {sw.ElapsedMilliseconds}");

            Console.WriteLine("Complete!!");
            Console.ReadLine();
        }
    }
}
