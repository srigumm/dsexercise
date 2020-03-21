using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q1.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Q1.Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void Should_Merge_Two_Valid_IntegerFiles()
        {
            //Arrange
            var localFileManager = new LocalFileManager();
            var expectedMergedFile = Path.Combine(Path.GetTempPath(), "Merged_File.txt");

            //cleanup.
            if (File.Exists(expectedMergedFile))
            {
                File.Delete(expectedMergedFile);
            }

            //Act
            using (var sortedFilesMergeUtil = new SortedFilesMergeUtil(localFileManager, new CompareUtil())) {
                sortedFilesMergeUtil.Merge(@"Data/IntegerDataFile1.txt", @"Data/IntegerDataFile2.txt");
            }

            //Assert
            File.Exists(expectedMergedFile).Should().BeTrue();
        }

        [TestMethod]
        public void Should_Merge_Two_Valid_DateTimeFiles()
        {
            //Arrange
            var localFileManager = new LocalFileManager();
            var expectedMergedFile = Path.Combine(Path.GetTempPath(), "Merged_File.txt");

            //cleanup.
            if (File.Exists(expectedMergedFile))
            {
                File.Delete(expectedMergedFile);
            }

            //Act
            using (var sortedFilesMergeUtil = new SortedFilesMergeUtil(localFileManager, new CompareUtil()))
            {
                sortedFilesMergeUtil.Merge(@"Data/DateTimeDataFile1.txt", @"Data/DateTimeDataFile2.txt");
            }

            //Assert
            File.Exists(expectedMergedFile).Should().BeTrue();
        }

        [TestMethod]
        public void Should_Merge_Two_Valid_StringDataFiles()
        {
            //Arrange
            var localFileManager = new LocalFileManager();
            var expectedMergedFile = Path.Combine(Path.GetTempPath(), "Merged_File.txt");

            //cleanup.
            if (File.Exists(expectedMergedFile))
            {
                File.Delete(expectedMergedFile);
            }

            //Act
            using (var sortedFilesMergeUtil = new SortedFilesMergeUtil(localFileManager, new CompareUtil()))
            {
                sortedFilesMergeUtil.Merge(@"Data/StringDataFile1.txt", @"Data/StringDataFile2.txt");
            }

            //Assert
            File.Exists(expectedMergedFile).Should().BeTrue();
        }
    }
}
