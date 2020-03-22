using Q2.DataPipeline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.DataPipeline
{
    public interface IStatsCalculator
    {
        CusipResult Calculate(CUSIP cusipData);
    }
}
