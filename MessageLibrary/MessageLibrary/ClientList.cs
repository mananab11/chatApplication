using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLibrary
{
    internal class ClientList
    {
        static internal Dictionary<string, IBroadcastCallBack> registeredClients = new Dictionary<string, IBroadcastCallBack>();
      //static internal  List<IBroadcastCallBack> registeredClients = new List<IBroadcastCallBack>();
      // static internal List<IBroadcastCallBack> unregisteredClients = new List<IBroadcastCallBack>();
        static internal string mg { get; set; }
        static internal int ActiveClients{get;set;}
    }
}
