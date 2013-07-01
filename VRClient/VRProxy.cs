using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;
using System.Threading;

namespace VRClient
{
    class VRProxy
    {
        string primaryAddress;
        MessageProcessor messageProcessor;
        Socket clientSocket { get; set; }
        Context context { get; set; }
        bool isListening;
        public CommandProcessor commandProcessor { get; set; }

        public VRProxy(string address, MessageProcessor messageProcessor)
        {
            primaryAddress = address;
            this.messageProcessor = messageProcessor;
            Context context = new Context();
            clientSocket = context.Socket(SocketType.REQ);
            clientSocket.Connect("tcp://" + primaryAddress);
            isListening = false;
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
            clientSocket.SendMore(BitConverter.GetBytes(message.messageID));
            clientSocket.SendMore(BitConverter.GetBytes(message.operation.operationID));
            clientSocket.SendMore(message.operation.path, Encoding.Unicode);
            if (message.operation.operationID == 1)
            {
                clientSocket.SendMore(message.operation.file);
            }
            clientSocket.SendMore(BitConverter.GetBytes(message.clientID));
            clientSocket.SendMore(BitConverter.GetBytes(message.requestNumber));
            clientSocket.Send(BitConverter.GetBytes(message.viewNumber));
            try
            {
                waitForReply(3 * 1000);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void waitForReply(int timeout)
        {
            Thread t = new Thread(receiveOperation);
            t.Start();
            if (!t.Join(timeout))
            {
                t.Abort();
                throw new System.Exception("Connection time out!");
            }
            
        }

        public void receiveOperation()
        {
            MessageReply reply = null;
            reply = receiveReply();
            messageProcessor.processMessage(reply);
        }

        public MessageReply receiveReply()
        {
            isListening = true;
            MessageReply reply = null;
            //while (isListening)
            {
                var messageID = BitConverter.ToInt32(clientSocket.Recv(), 0);
                var viewNumber = BitConverter.ToInt32(clientSocket.Recv(), 0);
                var requestNumber = BitConverter.ToInt32(clientSocket.Recv(), 0);
                var result = BitConverter.ToBoolean(clientSocket.Recv(), 0);
                reply = new MessageReply(messageID, viewNumber, requestNumber, result);
            }
            return reply;
        }
    }
}
