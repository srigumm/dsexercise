using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q1.Util;
using System;
using System.IO;
using System.Text;

namespace Q1.Test
{
    [TestClass]
    [System.Runtime.InteropServices.Guid("4D07F531-D415-476D-B054-C128DA773F27")]
    public class SortedFilesMergeUtilTest
    {
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString("1|3|6|"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString("2|4|5|"));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine,"|").Should().Be(@"1|2|3|4|5|6|");
        }
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData_Set1()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString("1|2|3|"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString("4|5|6|"));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|");
        }
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData_Set2()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString("1|2|3|4|5|"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString("6|"));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|");
        }
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData_Set3()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString("1|"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString("2|3|4|5|6|"));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|");
        }
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData_Set4()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString("|"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString("1|2|3|4|5|6|"));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|");
        }
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData_Set5()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString("1|2|3|4|5|6|"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString("|"));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|");
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
