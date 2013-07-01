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
    }
}
