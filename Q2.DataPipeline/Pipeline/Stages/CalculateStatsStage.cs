using Q2.DataPipeline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.DataPipeline.Pipeline.Stages
{
    public class CalculateStatsStage
    {
        private IStatsCalculator _statsCalculator;

        public CalculateStatsStage(IStatsCalculator statsCalculator)
        {
            _statsCalculator = statsCalculator;
        }

        public CusipResult CalculateStats(CUSIP cusipBlock)
        {
            Console.WriteLine($"Processing cusip - {cusipBlock.Id}");
            return _statsCalculator.Calculate(cusipBlock);
        }
    }
}
