using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderService
{
    public class ConsoleOutputHeadersMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();
            Message originalMessage = buffer.CreateMessage();
            foreach (MessageHeader h in originalMessage.Headers)
            {
                // Console.WriteLine("\n{0}\n", h);
                Console.WriteLine(h);
            }
            return null;

        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

            MessageBuffer buffer = reply.CreateBufferedCopy(0x7fffffff);
            reply = buffer.CreateMessage();
            Message originalMessage = buffer.CreateMessage();
            foreach (MessageHeader h in originalMessage.Headers)
            {
                //  Console.WriteLine("\n{0}\n", h);
                Console.WriteLine(h);
            }

        }
    }
}
