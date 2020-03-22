using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Q2.DataPipeline.Pipeline
{
    public interface IPipeline
    {
        Task RunAsync(string filePath);
    }
}
