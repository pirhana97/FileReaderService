using FileReaderService;
using FileReaderServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SautinSoft.Document;
using System.Net;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FileReaderServiceClient
{
    class Program
    {

        #region IDispatchMessageInspector Members
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("IDispatchMessageInspector.AfterReceiveRequest called.");
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Console.WriteLine("IDispatchMessageInspector.BeforeSendReply called.");
        }
        #endregion


        static void Main(string[] args)
        {
            IFileReaderServiceCallback callback = new FileReaderServiceCallback();
            InstanceContext context = new InstanceContext(callback);

            FileReaderServiceProxy.FileReaderServiceProxy proxy = new FileReaderServiceProxy.FileReaderServiceProxy(context);



            Console.WriteLine("Client is running at " + DateTime.Now.ToString());
            Console.WriteLine("Enter the filepath: ");
            string filePath = Console.ReadLine();
            Console.WriteLine(proxy.Echo(filePath));
            Console.WriteLine(proxy.GetFileAttributes(filePath));
            Console.WriteLine(proxy.PerCall_FileReader());

            Uri baseAddress = new Uri("http://localhost:8090/FileReaderWCFService/FileReaderService");
            ServiceHost serviceHost = new ServiceHost(typeof(FileReaderService.FileReaderService), baseAddress);

            ServiceEndpoint endpoint = serviceHost.AddServiceEndpoint(
                typeof(IFileReaderService),
                new WSHttpBinding(),
                "FileReaderServiceObject");


            Console.WriteLine("Address: {0}", endpoint.Address);

            // Enable Mex
            //  ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //  smb.HttpGetEnabled = true;
            //   serviceHost.Description.Behaviors.Add(smb);

            //   serviceHost.Open();
            //  endpoint.Behaviors.Add(new MyEndpointBehavior());


            Console.WriteLine("List all behaviors:");
              foreach (IEndpointBehavior behavior in endpoint.Behaviors)
             {
                 Console.WriteLine("Behavior: {0}", behavior.ToString());
              }





            string sessionID = proxy.InnerChannel.SessionId;
            Console.WriteLine("Session ID: "+sessionID);
            Console.ReadLine();

            


        }
    }
}


//string uriString;
//Console.Write("\nPlease enter the URI to post data to {for example, http://www.contoso.com} : ");
//            uriString = Console.ReadLine();
//// Create a new WebClient instance.
//WebClient myWebClient = new WebClient();
//Console.WriteLine("\nPlease enter the data to be posted to the URI {0}:", uriString);
//            string postData = Console.ReadLine();
//myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

//            // Display the headers in the request
//            Console.Write("Resulting Request Headers: ");
//            Console.WriteLine(myWebClient.Headers.ToString());

//            // Apply ASCII Encoding to obtain the string as a byte array.

//            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
//Console.WriteLine("Uploading to {0} ...", uriString);
//            // Upload the input string using the HTTP 1.0 POST method.
//            byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);

//// Decode and display the response.
//Console.WriteLine("\nResponse received was {0}",
//            Encoding.ASCII.GetString(responseArray));