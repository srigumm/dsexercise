using Q2.Model;
using SharedModules;
using System;
using System.Collections.Generic;

namespace Q2
{
    class Program
    {
        static void Main(string[] args)
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

            var fileManager = new LocalFileManager();
            var statsCalculator = new CusipStatsCalculator();
            var cusipFileUtil = new CusipFileUtil(fileManager, statsCalculator);
            IList<CusipResult> result = cusipFileUtil.ExtractStats(file1);

            foreach(var r in result)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine($"Cusip - {r.CUSIP}");
                Console.WriteLine($"Lowest - {r.Lowest}");
                Console.WriteLine($"Highest - {r.Highest}");
                Console.WriteLine($"Opening - {r.Opening}");
                Console.WriteLine($"Closing - {r.Closing}");
                Console.WriteLine("-------------------");
            }

            Console.WriteLine("Complete!!");
            Console.ReadLine();
        }
    }
}
