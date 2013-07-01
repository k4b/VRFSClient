using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VRClient
{
    class CommandProcessor
    {
        VRClient client { get; set; }
        VRProxy proxy { get; set; }

        public CommandProcessor(VRClient client, VRProxy proxy)
        {
            this.client = client;
            this.proxy = proxy;
        }

        public void startProcessing()
        {
            showHelp();
            while (true)
            {
                askCommand();
                string line = Console.ReadLine();
                string[] commandTokens = line.Split(' ');
                
                string command = commandTokens[0];
                if (command.Equals("exit"))
                {
                    return;
                }
                else if (command.Equals("help"))
                {
                    showHelp();
                }
                else if(command.Equals("copy"))
                {
                    commandTokens = new string[]
                    {
                        "copy",
                        "C:\\hosts.txt",
                        "hosts.txt"
                    };
                    processCopy(commandTokens);
                }
                else if (command.Equals("delete"))
                {
                    processDelete(commandTokens);
                }
                else
                {
                    Console.WriteLine("Wrong command!");
                }
                Console.WriteLine("");
            }
        }

        public void showHelp()
        {
            Console.WriteLine("#Commands:");
            Console.WriteLine("#Copy: copy src_file dest_file_in_root");
            Console.WriteLine("#Delete: delete file");
            Console.WriteLine("#Help: help");
            Console.WriteLine("#Exit: exit");
            Console.WriteLine("#Full paths to files");
        }

        public void askCommand()
        {
            Console.WriteLine("Enter command:");
            Console.WriteLine("");
            Console.Write(">");
        }

        private void processCopy(string[] commandTokens)
        {
            Console.WriteLine(commandTokens[0]);
            string srcPath = commandTokens[1];
            string destPath = commandTokens[2];
            byte[] bytes = File.ReadAllBytes(srcPath);
            Operation operationCopy = new Operation(bytes, destPath);
            MessageRequest request = new MessageRequest(1, operationCopy, client.ID, client.requestNumber, client.viewNumber);
            proxy.sendMessage(request);
        }

        private void processDelete(string[] commandTokens) 
        {
            Console.WriteLine(commandTokens[0]);
        }
    }
}
