using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace VRClient
{
    class VRProxy
    {
        string primaryAddress;
        Socket clientSocket { get; set; }
        Context context { get; set; }

        public VRProxy(string address)
        {
            primaryAddress = address;
            Context context = new Context();
            clientSocket = context.Socket(SocketType.REQ);
            clientSocket.Connect("tcp://" + primaryAddress);
        }

        public void startClient()
        {

            const string requestMessage = "1";
            Console.WriteLine("Sending request");
            clientSocket.Send(requestMessage, Encoding.Unicode);

            string reply = clientSocket.Recv(Encoding.Unicode);
            Console.WriteLine("Received reply: " + reply);
        }

        public void sendMessage(MessageRequest message)
        {
            Console.WriteLine("Sending request message");
            clientSocket.SendMore(BitConverter.GetBytes(message.messageID));
            clientSocket.SendMore(BitConverter.GetBytes(message.operation.operationID));
            clientSocket.SendMore(message.operation.path, Encoding.Unicode);
            clientSocket.SendMore(message.operation.file);
            clientSocket.SendMore(BitConverter.GetBytes(message.clientID));
            clientSocket.SendMore(BitConverter.GetBytes(message.reqestNumber));
            clientSocket.SendMore(BitConverter.GetBytes(message.viewNumber));
        }
    }
}
