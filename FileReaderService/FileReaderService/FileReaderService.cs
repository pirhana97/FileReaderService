using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;


namespace FileReaderService
{
   //[ServiceBehavior(ConcurrencyMode =ConcurrencyMode.Multiple)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode =ConcurrencyMode.Single, TransactionIsolationLevel = System.Transactions.IsolationLevel.Serializable,  
    TransactionTimeout = "00:00:45")]
    [TraceServiceBehavior]
 //  [ConsoleHeaderOutputBehavior]
    public class FileReaderService : IFileReaderService
    {
        int fileReader_Count = 0;




        [OperationBehavior(TransactionScopeRequired = true)]
        public string Echo(string input)
        {
            return $"Recieved message from client {input}";
        }


        [OperationBehavior(TransactionScopeRequired = true)]
        public string GetFileAttributes(string filePath)
        {
            FileInfo fi = new System.IO.FileInfo(filePath);

            int charCount = 0;
            int linesCount = 0;
            string FileText = new System.IO.StreamReader(filePath).ReadToEnd().Replace("\r\n", "\r");
            charCount = FileText.Length;
            linesCount = FileText.Split('\r').Length;

            long total_size_in_bytes = fi.Length;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Total Size: " + total_size_in_bytes + "\n");
            stringBuilder.Append("Character Count: " + FileText.Length + "\n");
            stringBuilder.Append("Line Count: " + FileText.Split('\r').Length + "\n");


            string fileDetails = stringBuilder.ToString();

            return fileDetails;
        }

        public string PerCall_FileReader()
        {
            fileReader_Count++;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Instance Context Provider 'Per Call' :"+fileReader_Count);

            string perCall = stringBuilder.ToString();

            return perCall;
        }
    }
}
