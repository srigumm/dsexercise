using Q2.DataPipeline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.DataPipeline
{
    public class CusipStatsCalculator : IStatsCalculator
    {
        public CusipResult Calculate(CUSIP cusipData)
        {
            if(cusipData == null || cusipData.PriceTicks == null || cusipData.PriceTicks.Count==0)
            {
                return new CusipResult(cusipData.Id);
            }

            double lowest = Double.MaxValue;
            double highest = Double.MinValue;
            
            double opening = cusipData.PriceTicks[0];
            double closing = cusipData.PriceTicks[cusipData.PriceTicks.Count - 1];
            foreach(var priceTick in cusipData.PriceTicks)
            {
                if (priceTick < lowest)
                {
                    lowest = priceTick;
                }
                if(priceTick > highest)
                {
                    highest = priceTick;
                }
            }

            return new CusipResult()
            {
                CUSIP = cusipData.Id,
                Lowest = lowest,
                Highest = highest,
                Opening = opening,
                Closing = closing
            };
        }
    }
}
