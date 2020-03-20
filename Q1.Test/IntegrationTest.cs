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
        public void Should_Merge_Two_Valid_Files()
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
            using (var sortedFilesMergeUtil = new SortedFilesMergeUtil(localFileManager)) {
                sortedFilesMergeUtil.Merge(@"Data/ValidFile1.txt", @"Data/ValidFile2.txt");
            }

            //Assert
            File.Exists(expectedMergedFile).Should().BeTrue();
        }
    }
}
