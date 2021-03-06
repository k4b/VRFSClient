﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRClient
{
    class MessageRequest : Message
    {
        public Operation operation { get; set; }
        public int clientID { get; set; }
        public int requestNumber { get; set; }
        public int viewNumber { get; set; }

        public MessageRequest(int messageID, Operation operation, int clientID, int requestNumber, int viewNumber)
        {
            this.messageID = messageID;
            this.clientID = clientID;
            this.requestNumber = requestNumber;
            this.viewNumber = viewNumber;
            this.operation = operation;
        }

        public override string ToString()
        {
            string s = "";
            s += "Message REQUEST" + Environment.NewLine;
            s += "ID: " + messageID + Environment.NewLine;
            s += "Operation: " + operation.operationID + Environment.NewLine;
            s += "Request number: " + requestNumber + Environment.NewLine;
            s += "View number: " + viewNumber + Environment.NewLine;
            return s;
        }
    }
}

