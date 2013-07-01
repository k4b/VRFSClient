using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMQ;

namespace VRClient
{
    class VRProxy
    {
        public void startClient(string address)
        {
            using (Context context = new Context())
            {
                using (Socket client = context.Socket(SocketType.REQ))
                {
                    client.Connect("tcp://" + address);

                    const string requestMessage = "Hello";
                    Console.WriteLine("Sending request");
                    client.Send(requestMessage, Encoding.Unicode);

                    string reply = client.Recv(Encoding.Unicode);
                    Console.WriteLine("Received reply: " + reply);
                }
            }
        }
    }
}
