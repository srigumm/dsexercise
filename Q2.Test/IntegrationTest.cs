using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q2.Model;
using SharedModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q2.Test
{
    [TestClass]
    public class IntegarionTest
    {
        [TestMethod]
        public void Should_ThrowException_For_InValid_InputFile()
        {
            //Arrange
            var fileManager = new LocalFileManager();
            var fileStatsCalculator = new CusipStatsCalculator();
            var cusipFileUtilObj = new CusipFileUtil(fileManager,fileStatsCalculator);
            var dataFile = @"NonExistingFile.txt";

            //Act
            Action act = () => cusipFileUtilObj.ExtractStats(dataFile);

            //Assert
            act.Should().Throw<Exception>().WithMessage("File doesn't exist!!");
        }
        [TestMethod]
        public void Should_GenerateResult_For_Valid_InputFile()
        {
            //Arrange
            var fileManager = new LocalFileManager();
            var fileStatsCalculator = new CusipStatsCalculator();
            var cusipFileUtilObj = new CusipFileUtil(fileManager,fileStatsCalculator);
            var dataFile = @"Data/ValidData.txt";

            //Act
            IList<CusipResult> results = cusipFileUtilObj.ExtractStats(dataFile);

            //Assert
            results.Should().NotBeNull();
            results.Count.Should().Be(2);
        }
    }
}
