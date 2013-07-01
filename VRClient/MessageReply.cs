using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRClient
{
    class MessageReply : Message
    {
        public int requestNumber { get; set; }
        public int viewNumber { get; set; }
        public bool result { get; set; }

        public MessageReply(int messageID, int viewNumber, int requestNumber, bool result)
        {
            this.messageID = messageID;
            this.requestNumber = requestNumber;
            this.viewNumber = viewNumber;
            this.result = result;
        }

        public override string ToString()
        {
            string s = "";
            s += "Message REPLY" + Environment.NewLine;
            s += "ID: " + messageID + Environment.NewLine;
            s += "Request number: " + requestNumber + Environment.NewLine;
            s += "View number: " + viewNumber + Environment.NewLine;
            s += "Result: " + result + Environment.NewLine;
            return s;
        }
    }
}
