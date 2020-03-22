using Q2.Model;
using SharedModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2
{
    public class CusipFileUtil : ICusipFileUtil
    {
        private IFileManager _fileManager;
        private IStatsCalculator _statsCalculator;

        public CusipFileUtil(IFileManager fileManager,IStatsCalculator statsCalculator)
        {
            _fileManager = fileManager;
            _statsCalculator = statsCalculator;
        }

        public IList<CusipResult> ExtractStats(string inputFilePath)
        {
            IList<CUSIP> cusipData = new List<CUSIP>();
            IList<CusipResult> cusipResult = new List<CusipResult>();
            IList<double> priceTicks = new List<double>();
            string previousCusip = string.Empty;
            string currentCusip = string.Empty;

            using (var file1Reader = _fileManager.ReadAsync(inputFilePath)) //stream content without reading entire file into memory
            {
                while (!file1Reader.EndOfStream)
                {
                    var currentRow = file1Reader.ReadLine();

                    //Type typeOfCurrentRow = _fileManager.DiscoverTypeOfData(currentRow);
                    if (!isDouble(currentRow))
                    {
                        currentCusip = currentRow;

                        //save cusip block that we read so far.
                        if (!string.IsNullOrEmpty(previousCusip) && priceTicks.Count>0)
                        {
                            var extractedCusip = new CUSIP() { Id = previousCusip, PriceTicks = priceTicks };
                            cusipData.Add(extractedCusip);

                            cusipResult.Add(_statsCalculator.Calculate(extractedCusip));
                        }

                        priceTicks = new List<double>();//re-intialize 
                        previousCusip = currentCusip;
                    }
                    else
                    {
                        priceTicks.Add(Double.Parse(currentRow));
                    }
                }
                if (priceTicks.Count > 0)
                {
                    var extractedCusip = new CUSIP() { Id = currentCusip, PriceTicks = priceTicks };
                    cusipData.Add(extractedCusip);
                    cusipResult.Add(_statsCalculator.Calculate(extractedCusip));
                }
            }
            return cusipResult;
        }

        private bool isIntegerData(Type typeOfCurrentRow)
        {
            return typeOfCurrentRow == typeof(double);
        }
        private bool isDouble(string data)
        {
            double i = -1;
            return Double.TryParse(data, out i);
        }
    }
}
