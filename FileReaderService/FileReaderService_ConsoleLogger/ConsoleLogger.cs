using FileReaderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderService_ConsoleLogger
{
    public class ConsoleLogger
    {
       

        public void print_To_Client_FileReaderService()
        {
            Console.WriteLine("Client is running at " + DateTime.Now.ToString());
            Console.WriteLine("Enter the filepath: ");
        }

        public void print_File_Info()
        {

            IFileReaderServiceCallback callback = new FileReaderServiceCallback();
            InstanceContext context = new InstanceContext(callback);
            FileReaderServiceProxy.FileReaderServiceProxy proxy = new FileReaderServiceProxy.FileReaderServiceProxy(context);

            string filePath = Console.ReadLine();
            Console.WriteLine(proxy.Echo(filePath));
            Console.WriteLine(proxy.GetFileAttributes(filePath));
            Console.WriteLine(proxy.PerCall_FileReader());

        }

        public void print_Session_Info()
        {
            IFileReaderServiceCallback callback = new FileReaderServiceCallback();
            InstanceContext context = new InstanceContext(callback);
            FileReaderServiceProxy.FileReaderServiceProxy proxy = new FileReaderServiceProxy.FileReaderServiceProxy(context);

            Uri baseAddress = new Uri("http://localhost:8090/FileReaderWCFService/FileReaderService");
            ServiceHost serviceHost = new ServiceHost(typeof(FileReaderService.FileReaderService), baseAddress);



            ServiceEndpoint endpoint = serviceHost.AddServiceEndpoint(
                typeof(IFileReaderService),
                new WSHttpBinding(),
                "FileReaderServiceObject");


            Console.WriteLine("Address: {0}", endpoint.Address);
            string sessionID = proxy.InnerChannel.SessionId;
            Console.WriteLine("Session ID: " + sessionID);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

    }
}
