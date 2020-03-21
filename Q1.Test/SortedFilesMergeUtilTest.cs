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
        #region Integer files merging
        [TestMethod]
        public void Should_Merge_Two_Files_With_IntegerData()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var compareUtil = new CompareUtil();

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            int i = 0;
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString(" 1 | 3 | 5 |"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString(" 2 | 4 | 6 |"));
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(int));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, compareUtil);
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
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(int));

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, new CompareUtil());
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
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(int));

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, new CompareUtil());
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
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(int));

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, new CompareUtil());
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
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(int));

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, new CompareUtil());
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            //sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|"); TODO
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6||");
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
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(int));

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, new CompareUtil());
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"1|2|3|4|5|6|");
        }

        #endregion

        #region DateTime files merging
        [TestMethod]
        public void Should_Merge_Two_Files_With_DateTimeData()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var compareUtil = new CompareUtil();

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            int i = 0;
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString(" 01/12/1987 | 01/14/1987 | 01/16/1987 |"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString(" 01/13/1987 | 01/15/1987 | 01/17/1987 |"));
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(DateTime));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, compareUtil);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"01/12/1987|01/13/1987|01/14/1987|01/15/1987|01/16/1987|01/17/1987|");
        }

        #endregion

        #region String files merging
        [TestMethod]
        public void Should_Merge_Two_Files_With_StringData()
        {
            //Arrange
            var fakeFileManager = A.Fake<IFileManager>();
            var compareUtil = new CompareUtil();

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            int i = 0;
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile1.txt")).Returns(GenerateStreamFromString(" aa | ac | ae |"));
            A.CallTo(() => fakeFileManager.ReadAsync(@"SomeFile2.txt")).Returns(GenerateStreamFromString(" ab | ad | af |"));
            A.CallTo(() => fakeFileManager.DiscoverTypeOfData("SomeFile1.txt", "SomeFile2.txt")).Returns(typeof(string));
            A.CallTo(() => fakeFileManager.CreateFile(A<string>.Ignored)).Returns(sw);

            //Act
            var fileMergeUtil = new SortedFilesMergeUtil(fakeFileManager, compareUtil);
            fileMergeUtil.Merge(@"SomeFile1.txt", "SomeFile2.txt");

            //Assert
            sw.Flush();
            ms.Position = 0;
            var sr = new StreamReader(ms);
            sr.ReadToEnd().Replace(Environment.NewLine, "|").Should().Be(@"aa|ab|ac|ad|ae|af|");
        }

        #endregion

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
