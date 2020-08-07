using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderService
{
      public interface IFileReaderServiceCallback
        {
            [OperationContract]
            void OnCallback();
        }

        [ServiceContract(CallbackContract = typeof(IFileReaderServiceCallback))]
        public interface IFileReaderService
        {
        [OperationContract()]
        string Echo(string input);

        [OperationContract()]
        string GetFileAttributes(string filePath);

        [OperationContract()]
        string PerCall_FileReader();
        }


    
}
