using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileReaderService;
//using NUnit.Framework;
using Moq;
using System.IO;

namespace FileReaderService_uTest
 
{
    [TestClass]
    public class FileReaderServiceTests
    {
        [TestMethod]
        public void GetFileAttributes_NullValues_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                FileReaderService.FileReaderService sut = new FileReaderService.FileReaderService();

                var result = sut.GetFileAttributes(null);
            }, "ArgumentNullException Thrown");
        }

        [TestMethod]
        public void Echo_NullValues_ThrowsNotNullException()
        {
            var sut = new FileReaderService.FileReaderService();
            Assert.IsNotNull(sut.Echo(It.IsAny<string>()));
        }

        [TestMethod]
        public void PerCall_IsNotNull_AssertNotNull()
        {
            var sut = new FileReaderService.FileReaderService();
            Assert.IsNotNull(sut.PerCall_FileReader());

        }

        [TestMethod]
        public void IFileReaderService_ReturnsNull_()
        {
            Mock<IFileReaderService> mock = new Mock<IFileReaderService>();
            mock.Setup(x => x.GetFileAttributes(It.IsAny<string>())).Returns(() => null);


        }
    }
}
