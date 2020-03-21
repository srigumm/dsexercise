using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q1.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q1.Test.Util
{
    [TestClass]
    public class CompareUtilTest
    {
        [TestMethod]
        public void Should_Return_Comparer_For_Ints()
        {
            //Arrange
            var compareUtil = new CompareUtil();

            //Act
            Func<string, string, bool> func = compareUtil.getComparer(typeof(int));

            //Assert
            func("3", "5").Should().BeTrue();
            func("3", "3").Should().BeTrue();
            func("5", "3").Should().BeFalse();
        }

        [TestMethod]
        public void Should_Return_Comparer_For_DateTime()
        {
            //Arrange
            var compareUtil = new CompareUtil();

            //Act
            Func<string, string, bool> func = compareUtil.getComparer(typeof(DateTime));

            //Assert
            func("07/18/1987", "07/19/1987").Should().BeTrue();
            func("07/13/1987", "07/14/1987").Should().BeTrue();
            func("07/18/1987", "07/18/1987").Should().BeTrue();
            func("07/18/1989", "07/19/1987").Should().BeFalse();
        }

        [TestMethod]
        public void Should_Return_Comparer_For_Strings()
        {
            //Arrange
            var compareUtil = new CompareUtil();

            //Act
            Func<string, string, bool> func = compareUtil.getComparer(typeof(string));

            //Assert
            func("ab", "ac").Should().BeTrue();
            func("ab", "ab").Should().BeTrue();
            func("ac", "ab").Should().BeFalse();
        }
    }
}
