using Q2.DataPipeline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.DataPipeline.Pipeline.Stages
{
    public class DisplayCusipStatsStage
    {
        public DisplayCusipStatsStage()
        {

        }
        public bool Display(CusipResult result)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine($"Cusip - {result.CUSIP}");
            Console.WriteLine($"Lowest - {result.Lowest}");
            Console.WriteLine($"Highest - {result.Highest}");
            Console.WriteLine($"Opening - {result.Opening}");
            Console.WriteLine($"Closing - {result.Closing}");
            Console.WriteLine("-------------------");

            return true;
        }
    }
}
