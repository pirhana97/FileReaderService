﻿
using FileReaderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderServiceProxy
{


    public class FileReaderServiceProxy : DuplexClientBase<IFileReaderService>, IFileReaderService
    {
        public FileReaderServiceProxy(InstanceContext context) : base(context)
        {

        }

        public string Echo(string input)
        {
            return base.Channel.Echo(input);
        }

        public string GetFileAttributes(string filePath)
        {
            return base.Channel.GetFileAttributes(filePath);
        }



        public string PerCall_FileReader()
        {
            return base.Channel.PerCall_FileReader();
        }
    }

}