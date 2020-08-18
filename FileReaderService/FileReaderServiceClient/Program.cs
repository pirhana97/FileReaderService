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
using FileReaderService_ConsoleLogger;

namespace FileReaderServiceClient
{





    public class Program
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

            string request = string.Empty;
            string response = string.Empty;

            FileReaderService.FileReaderService sc = new FileReaderService.FileReaderService();
            

            


            using (FileReaderServiceProxy.FileReaderServiceProxy proxy = new FileReaderServiceProxy.FileReaderServiceProxy(context))
            { 
                ConsoleLogger logger = new ConsoleLogger();

                try
                {
                    
                    logger.print_To_Client_FileReaderService();

                    logger.print_File_Info();

                    logger.print_Session_Info();

                }

                catch (FaultException<ApplicationException> e)
                {
                    Console.WriteLine("FaultException<>: " + e.Detail.GetType().Name + " - " + e.Detail.Message);
                }
                catch (FaultException e)
                {
                    Console.WriteLine("FaultException: " + e.GetType().Name + " - " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("EXCEPTION: " + e.GetType().Name + " - " + e.Message);

                }

            }



            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

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



// Enable Mex
//  ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
//  smb.HttpGetEnabled = true;
//   serviceHost.Description.Behaviors.Add(smb);

//   serviceHost.Open();
//  endpoint.Behaviors.Add(new MyEndpointBehavior());


//  Console.WriteLine("List all behaviors:");
// foreach (IEndpointBehavior behavior in endpoint.Behaviors)
//{
//    Console.WriteLine("Behavior: {0}", behavior.ToString());
// }