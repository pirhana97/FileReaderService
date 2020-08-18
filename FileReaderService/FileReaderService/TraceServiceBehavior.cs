using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace FileReaderService
{

    public class MessageTrace : IDispatchMessageInspector
    {
        private Message TraceMessage(MessageBuffer buffer)
        {
            Message msg = buffer.CreateMessage();
            StringBuilder sb = new StringBuilder("Message Content :");
            sb.Append(msg.ToString());
            Console.WriteLine(sb.ToString());

            string message = sb.ToString();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(message);
            xmlDocument.Save("SOAPXML.xml");

           // StringReader stringReader = new StringReader(message);
            


            return buffer.CreateMessage();
          
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
          //  request = TraceMessage(request.CreateBufferedCopy(Int32.MaxValue));
            Console.WriteLine("Request Received");

            

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
           //  reply = TraceMessage(reply.CreateBufferedCopy(Int32.MaxValue));
            Console.WriteLine("Sending Reply..");
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
