using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q2.Model;
using SharedModules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Q2.Test
{
    [TestClass]
    public class CusipFileUtilTest
    {
        [TestMethod]
        public void Should_Ignore_Cusip_With_NoPriceTicks()
        {
            //Arrange
            var fileManager = A.Fake<IFileManager>();
            var fileStatsCalculator = new CusipStatsCalculator();
            var cusipFileUtilObj = new CusipFileUtil(fileManager, fileStatsCalculator);

            A.CallTo(() => fileManager.Read("SomeValidFile.txt")).Returns(GenerateStreamFromString("DUMMYCUSIP-With-NoData1|CUSIP-1|11.11|21.56|9.3|17.18|99.99|DUMMYCUSIP-With-NoData-2|"));

            //Act
            IList<CusipResult> results = cusipFileUtilObj.ExtractStats("SomeValidFile.txt");

            //Assert
            results.Should().NotBeNull();
            results.Count.Should().Be(1);
            results.First().CUSIP.Should().Be("CUSIP-1");
        }

        [TestMethod]
        public void Should_GenerateResult_For_Valid_Inputs()
        {
            //Arrange
            var fileManager = A.Fake<IFileManager>();
            var fileStatsCalculator = new CusipStatsCalculator();
            var cusipFileUtilObj = new CusipFileUtil(fileManager,fileStatsCalculator);

            A.CallTo(() => fileManager.Read("SomeValidFile.txt")).Returns(GenerateStreamFromString("CUSIP-1|11.11|21.56|9.3|17.18|99.99|"));

            //Act
            IList<CusipResult> results = cusipFileUtilObj.ExtractStats("SomeValidFile.txt");

            //Assert
            results.Should().NotBeNull();
            results.Count.Should().Be(1);
            results.First().CUSIP.Should().Be("CUSIP-1");
            results.First().Lowest.Should().Be(9.3);
            results.First().Highest.Should().Be(99.99);
            results.First().Opening.Should().Be(11.11);
            results.First().Closing.Should().Be(99.99);
        }

        [TestMethod]
        public void Should_GenerateResult_For_Valid_Inputs_Case1()
        {
            //Arrange
            var fileManager = A.Fake<IFileManager>();
            var fileStatsCalculator = new CusipStatsCalculator();
            var cusipFileUtilObj = new CusipFileUtil(fileManager, fileStatsCalculator);

            A.CallTo(() => fileManager.Read("SomeValidFile.txt")).Returns(GenerateStreamFromString("CUSIP-1|11.11|21.56|9.3|17.18|99.99|CUSIP-2|22.22|0.5|0.1|5.8|88.88|"));

            //Act
            IList<CusipResult> results = cusipFileUtilObj.ExtractStats("SomeValidFile.txt");

            //Assert
            results.Should().NotBeNull();
            results.Count.Should().Be(2);
        }

        private static StreamReader GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            string[] records = s.Split('|', StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in records)
            {
                writer.WriteLine(line);
            }
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }
    }
}
