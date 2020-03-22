using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q2.Model;
using System.Collections.Generic;

namespace Q2.Test
{
    [TestClass]
    public class CusipStatsCalculatorTest
    {
        [TestMethod]
        public void Should_Calculate_Stats_For_A_Valid_CusipData()
        {
            //Arrange
            var cusipObject = new CUSIP()
            {
                Id = "cusip-1",
                PriceTicks = new List<double>()
                {
                    11.11,
                    10.05,
                    20.10,
                    10.05,
                    30.15,
                    10.05,
                    40.20,
                    33.33

                }
            };
            var objStatsCalculator = new CusipStatsCalculator();

            //Act
            CusipResult result = objStatsCalculator.Calculate(cusipObject);

            //Assert
            result.CUSIP.Should().Be("cusip-1");
            result.Lowest.Should().Be(10.05);
            result.Highest.Should().Be(40.20);
            result.Opening.Should().Be(11.11);
            result.Closing.Should().Be(33.33);
        }

        [TestMethod]
        public void Should_Handle_Empty_CusipData()
        {
            //Arrange
            var cusipObject = new CUSIP()
            {
                Id = "cusip-1",
                PriceTicks = null
            };
            var objStatsCalculator = new CusipStatsCalculator();

            //Act
            CusipResult result = objStatsCalculator.Calculate(cusipObject);

            //Assert
            result.CUSIP.Should().Be("cusip-1");
            result.Lowest.Should().Be(-1);
            result.Highest.Should().Be(-1);
            result.Opening.Should().Be(-1);
            result.Closing.Should().Be(-1);
        }
    }
}
