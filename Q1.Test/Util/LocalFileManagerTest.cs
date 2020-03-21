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

        [TestMethod]
        public void Should_Discover_DataType_From_FileContent()
        {
            //Arrange
            var validFilePath = @"Data/ValidFile1.txt";
            var fileManager = new LocalFileManager();

            //Act
            Type typeOfData = fileManager.DiscoverTypeOfData(validFilePath,"we_dont_look_at_seconf_file_data_TODO");

            //Assert
            typeOfData.Should().Be(typeof(int));
        }

        [TestMethod]
        public void Should_Parse_DateTime()
        {
            //Arrange
            var input = "07/18/1987";
            var fileManager = new LocalFileManager();

            //Act
            Type dataType = fileManager.DiscoverTypeOfData(input);

            //Assert
            dataType.Should().Be(typeof(DateTime));
        }
        [TestMethod]
        public void Should_Parse_Integers()
        {
            //Arrange
            var input = "123";
            var fileManager = new LocalFileManager();


            //Act
            Type dataType = fileManager.DiscoverTypeOfData(input);

            //Assert
            dataType.Should().Be(typeof(int));
        }

        [TestMethod]
        public void Should_Parse_Strings()
        {
            //Arrange
            var input = "ABCDE";
            var fileManager = new LocalFileManager();

            //Act
            Type dataType = fileManager.DiscoverTypeOfData(input);

            //Assert
            dataType.Should().Be(typeof(string));
        }
    }
}
