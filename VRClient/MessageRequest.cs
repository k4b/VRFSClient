using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRClient
{
    class MessageRequest : Message
    {
        public Operation operation { get; set; }
        public int clientID { get; set; }
        public int reqestNumber { get; set; }
        public int viewNumber { get; set; }

        public MessageRequest(int messageID, Operation operation, int clientID, int requestNumber, int viewNumber)
        {
            this.messageID = messageID;
            this.clientID = clientID;
            this.reqestNumber = reqestNumber;
            this.viewNumber = viewNumber;
            this.operation = operation;
        }
    }
}

