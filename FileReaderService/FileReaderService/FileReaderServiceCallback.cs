using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReaderService;

namespace FileReaderService
{
    public class FileReaderServiceCallback : IFileReaderServiceCallback
    {
        public void OnCallback()
        {
            Console.WriteLine("Callback from client side...");
        }
    }
}
