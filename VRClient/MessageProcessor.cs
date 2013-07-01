﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRClient
{
    class MessageProcessor
    {
        public CommandProcessor commandProcessor {get; set; }
        
        public void processMessage(MessageReply reply)
        {
            Console.WriteLine("Received message:");
            Console.WriteLine(reply.ToString());
            commandProcessor.askCommand();
        }
    }
}
