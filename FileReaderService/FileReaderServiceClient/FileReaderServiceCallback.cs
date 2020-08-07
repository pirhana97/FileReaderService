﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReaderService;

namespace FileReaderServiceClient
{
    class FileReaderServiceCallback : IFileReaderServiceCallback
    {
        public void OnCallback()
        {
            Console.WriteLine("Callback method is called from client side.");
        }
    }
}
