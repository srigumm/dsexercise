using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q1.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q1.Test.Util
{
    [TestClass]
    public class LocalFileManagerTest
    {
        [TestMethod]
        public void Should_Throw_Exception_For_InvalidFiles()
        {
            //Arrange
            var invalidFilePath = "InvalidFIle.txt";
            var fileManager = new LocalFileManager();

            //Act
            Action act = () => fileManager.ReadAsync(invalidFilePath);

            //Assert
            act.Should().Throw<Exception>().WithMessage("File doesn't exist!!");

        }

        [TestMethod]
        public void Should_Return_StreamReader()
        {
            //Arrange
            var validFilePath = @"Data/ValidFile1.txt";
            var fileManager = new LocalFileManager();


            //Act
            var reader = fileManager.ReadAsync(validFilePath);

            //Assert
            reader.Should().NotBeNull();
        }
    }
}
