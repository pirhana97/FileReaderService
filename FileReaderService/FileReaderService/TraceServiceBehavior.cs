using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections;
using System.Runtime.Serialization;

namespace FileReaderService
{

    public class MessageTrace : IDispatchMessageInspector
    {
        private Message TraceMessage(MessageBuffer buffer)
        {
            Message msg = buffer.CreateMessage();
            StringBuilder sb = new StringBuilder();
            sb.Append(msg.ToString());

            int length = msg.ToString().Length;
            if(length > 1000)
            {
                sb.Append("FileSize"+length);
            }

            Console.WriteLine(sb.ToString());

    
            return buffer.CreateMessage(); ;
          
          
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            //  request = TraceMessage(request.CreateBufferedCopy(Int32.MaxValue));
            Console.WriteLine("\nRequest Received");


            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("\nSending Reply..");
            //  reply = TraceMessage(reply.CreateBufferedCopy(Int32.MaxValue));
            string rep = reply.ToString();
           XmlDocument xmlDocument = new XmlDocument();
           xmlDocument.LoadXml(rep);


            XmlNode root = xmlDocument.FirstChild;
            if (root.HasChildNodes)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    Console.WriteLine(root.ChildNodes[i].InnerText);
                    
                }
            }
            int length = reply.ToString().Length;
            try
            {
                if (length > 1000)
                {
                    Console.WriteLine("File Size optimal:"+length);
                }
            }
            catch
            {
                Console.WriteLine("File Size less than 100 MB");
            }
            //  Console.WriteLine(rep);

        }

    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TraceMessageBehavior : Attribute, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            MessageTrace inspector = new MessageTrace();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        { }
    }

    public class TraceMessageBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(TraceMessageBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new TraceMessageBehavior();
        }
    }





    [AttributeUsage(AttributeTargets.Class)]
    class TraceServiceBehavior : Attribute, IServiceBehavior
    {


        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher eDispatcher in cDispatcher.Endpoints)
                {
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(new MessageTrace());
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }


}
