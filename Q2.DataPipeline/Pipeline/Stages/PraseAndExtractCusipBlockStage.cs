using Q2.DataPipeline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.DataPipeline.Pipeline.Stages
{
    public class PraseAndExtractCusipBlockStage
    {
        public PraseAndExtractCusipBlockStage()
        {

        }

        public CUSIP Parse(IList<string> rawCusipBlock)
        {
            /*
                Sample Input:
                    - First line will alwyas be CUSIP ID
                    - Subsequents lines represent price ticks.
              
                 CUSIP-1
                 11.11
                 10.05
                 20.10
                 10.05
                 30.15
                 10.05
                 40.20
                 33.33
             */
            var cusipObj = new CUSIP();

            if (rawCusipBlock.Count > 0)
            {
                Console.WriteLine($"Parsing cusip - {rawCusipBlock[0]}");
                cusipObj.Id = rawCusipBlock[0];
                cusipObj.PriceTicks = new List<Double>();

                for(int i = 1; i < rawCusipBlock.Count; i++)
                {
                    cusipObj.PriceTicks.Add(Double.Parse(rawCusipBlock[i]));
                }

                return cusipObj;
            }
            return null;
        }
    }
}
