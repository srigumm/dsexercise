using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.Model
{
    public class CusipResult
    {
        public string CUSIP { get; set; }
        public double Lowest { get; set; }
        public double Highest { get; set; }
        public double Opening { get; set; }
        public double Closing { get; set; }

        public CusipResult()
        {
        }
        public CusipResult(string cusip)
        {
            CUSIP = cusip;
            Lowest = -1;
            Highest = -1;
            Opening = -1;
            Closing = -1;
        }
    }
}
