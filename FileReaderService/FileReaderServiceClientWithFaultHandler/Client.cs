using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ServiceModel;
using FileReaderService;
using FileReaderServiceProxy;

namespace FileReaderServiceClientWithFaultHandler
{


    class Client : FileReaderService.FileReaderServiceCallback
    {
        AutoResetEvent waitHandle;

        public Client()
        {
            waitHandle = new AutoResetEvent(false);
        }

        public void Run()
        {
            // Picks up configuration from the configuration file.
            //  FileReaderServiceClient wcfClient
            //    = new FileReaderServiceClient(new InstanceContext(this), "FileReaderService.FileReaderService");

            IFileReaderServiceCallback callback = new FileReaderServiceCallback();
            InstanceContext context = new InstanceContext(callback);

            FileReaderServiceProxy.FileReaderServiceProxy proxy = new FileReaderServiceProxy.FileReaderServiceProxy(context);

            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter a filepath and press ENTER: ");
                Console.Write(">>> ");
                Console.ForegroundColor = ConsoleColor.Green;
                string filePath = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Called service with: \r\n\t" + filePath);

                proxy.Echo(filePath);

            //    wcfClient.Hello(greeting);
                Console.WriteLine("Execution passes service call and moves to the WaitHandle.");
                this.waitHandle.WaitOne();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Set was called.");
                Console.Write("Press ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ENTER");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" to exit...");
                Console.ReadLine();
            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine("The service operation timed out. " + timeProblem.Message);
                Console.ReadLine();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine("There was a communication problem. " + commProblem.Message);
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            Client client = new Client();
            client.Run();
        }
    }
}
