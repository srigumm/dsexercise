using Q2.DataPipeline.Model;
using Q2.DataPipeline.Pipeline.Stages;
using SharedModules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Q2.DataPipeline.Pipeline
{
    public class CusipDataPipeline : IPipeline
    {
        private IFileManager _fileManager;
        private readonly IStatsCalculator _statsCalculator;
        private TransformBlock<IList<string>,CUSIP> _headStep;

        public CusipDataPipeline(IFileManager fileManager, IStatsCalculator statsCalculator)
        {
            _fileManager = fileManager;
            _statsCalculator = statsCalculator;
            _headStep = CreatePipeline();
        }
        public async System.Threading.Tasks.Task RunAsync(string filePath)
        {
            /* stages for Cusip Datapipeline workflow
             *    _______          _________________           _________________          __________  
             *   |       |       |                   |        |                 |        |          | 
             *   | Read  | ====> | Parse And Extract | ====>  | Calculate Stats | ====>  |  Display | 
             *   |_______|       | _________________ |        |_________________|        |__________| 
             *                                                      
             */
            IList<string> cusipBlock = new List<string>();
            string previousCusip = string.Empty;
            using (var file1Reader = _fileManager.Read(filePath)) //stream content without reading entire file into memory
            {
                
                while (!file1Reader.EndOfStream)
                {
                    var currentRow = await file1Reader.ReadLineAsync();

                    if (!isDouble(currentRow))
                    {
                        string currentCusip = currentRow;

                        //save cusip block that we read so far.
                        if (!string.IsNullOrEmpty(previousCusip)&&cusipBlock.Count>0)
                        {
                            Console.WriteLine("Submitted a cusip block for processing");
                            _headStep.Post(cusipBlock); //Push block by block.
                        }
                        cusipBlock = new List<String>();
                        cusipBlock.Add(currentRow);
                        previousCusip = currentCusip;
                    }
                    else
                    {
                        cusipBlock.Add(currentRow);
                    }
                }
                if (cusipBlock.Count > 0)
                {
                    Console.WriteLine("Submitted a cusip block for processing");
                    _headStep.Post(cusipBlock);
                }
            }
        }

        public  TransformBlock<IList<string>, CUSIP> CreatePipeline()
        {
            //Deserialize raw cusip block to CUSIP type
            var step1 = new TransformBlock<IList<string>, CUSIP>(new PraseAndExtractCusipBlockStage().Parse);

            //Run calculations on CUSIP object
            var step2 = new TransformBlock<CUSIP, CusipResult>(new CalculateStatsStage(_statsCalculator).CalculateStats);

            //Display CUSIP stats to console
            var step3 = new TransformBlock<CusipResult,bool>(new DisplayCusipStatsStage().Display,
                                                                new ExecutionDataflowBlockOptions()
                                                                {
                                                                    MaxDegreeOfParallelism = 1, //we dont want multiple threads for display
                                                                    BoundedCapacity = 13,
                                                                });

            step1.LinkTo(step2, new DataflowLinkOptions());
            step2.LinkTo(step3, new DataflowLinkOptions());
            return step1;
        }

        private bool isDouble(string data)
        {
            double i = -1;
            return Double.TryParse(data, out i);
        }
    }
}
