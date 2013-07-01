using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRClient
{
    class VRClient
    {
        //Client Parameters
        public int ID { get; private set; }
        public string address { get; private set; }
        public ReplicaInfo primary { get; set; }
        public List<ReplicaInfo> replicaList { get; set; }
        private int viewNumber { get; set; }

        public VRClient()
        {
            readAndSetHosts();
            readAndSetParameters();
            primary = replicaList[0];
            viewNumber = 1;

            Console.WriteLine(identify());
            VRProxy proxy = new VRProxy();
            proxy.startClient(primary.address);
        }

        public void readAndSetHosts()
        {
            List<string> hosts = new List<string>(System.IO.File.ReadAllLines(@"hosts.txt"));
            List<ReplicaInfo> replicas = new List<ReplicaInfo>();
            for (int i = 0; i < hosts.Count; i++)
            {
                ReplicaInfo r = new ReplicaInfo(i + 1, hosts[i]);
                replicas.Add(r);
            }
            replicaList = replicas;
        }

        public void readAndSetParameters()
        {
            List<string> parameters = new List<string>(System.IO.File.ReadAllLines(@"Parameters.txt"));
            ID = Convert.ToInt32(parameters[0]);
            address = parameters[1];
        }

        public string identify()
        {
            string s = "";
            s += "CLient app" + Environment.NewLine;
            s += "ID: " + ID + Environment.NewLine;
            s += "IPAddress: " + address + Environment.NewLine;
            s += "Hosts: " + Environment.NewLine;
            foreach (ReplicaInfo rep in replicaList)
            {
                s += "Replica: " + rep.ID + " " + rep.address + Environment.NewLine;
            }
            return s;
        }
    }
}
