using Q2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2
{
    public interface ICusipFileUtil
    {
        IList<CusipResult> ExtractStats(string inputFilePath);
    }
}
