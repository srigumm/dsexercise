using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.Model
{
    public class CUSIP
    {
        public string Id { get; set; }
        public IList<double> PriceTicks { get; set; }
    }
}
