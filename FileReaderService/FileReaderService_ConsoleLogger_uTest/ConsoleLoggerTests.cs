using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FileReaderService_ConsoleLogger_uTest
{
    [TestClass]
    public class ConsoleLoggerTests
    {
        [TestMethod]
        public void ConsoleLogger_PrintValues_Throw_Exception()
        {
            var sut = new FileReaderService_ConsoleLogger.ConsoleLogger();
            try
            {
                sut.print_To_Client_FileReaderService();

            }

            catch (Exception ex)
            {
                Assert.IsFalse(true);
            }
            finally
            {
                Assert.IsTrue(true);
            }
        }
       
    }
}
