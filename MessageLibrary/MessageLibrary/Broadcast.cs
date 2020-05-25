using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
namespace MessageLibrary
{   //delegate to count active users
    public delegate void usersCount();
     
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,ConcurrencyMode = ConcurrencyMode.Single)]
    public class Broadcast : IBroadcast
    {       //event to check any change in number of active users
        public event usersCount UserCountChanges;
       
        public  string clientName;
        
        void IBroadcast.RegisterClient(string name,string id)
        {

            ClientList.registeredClients.Add(id,OperationContext.Current.GetCallbackChannel<IBroadcastCallBack>());
                clientName = name;
            //event call
                ChangeEvent();
        }

       

        void IBroadcast.UnRegisterClient(string id)
        {
            ClientList.registeredClients.Remove(id/*OperationContext.Current.GetCallbackChannel<IBroadcastCallBack>()*/);
            //event call
            ChangeEvent();
        }
        private void ChangeEvent()
        {
            UserCountChanges += new usersCount(UserActiveCount);
            UserCountChanges.Invoke();
        }
        void UserActiveCount()
        {   //get count from activeclientslist
             
            foreach (IBroadcastCallBack client in ClientList.registeredClients.Values)
            {
                client.ActiveUsers(ClientList.registeredClients.Count);
            }
            
        }

        void IBroadcast.SendMessage(string msg,List<string> memberId)
        {
            lock (this)
            { 

                ClientList.mg = msg;
            }
            BroadcastMessage(ClientList.mg,clientName,memberId);
        }

        
        public void BroadcastMessage(string message,string name,List<string> memberId)
        {
           Thread.Sleep(1000);
           // OperationContext.Current.GetCallbackChannel<IBroadcastCallBack>().SendToAll(message);
           try
           {
                foreach (string  idnumber in memberId)
                {
                    foreach (var clientkeypair in ClientList.registeredClients.Where(clientkeypair => clientkeypair.Key == idnumber))
                    {
                        clientkeypair.Value.SendToAll(message, name);
                    }

                }
               //foreach (IBroadcastCallBack client in ClientList.registeredClients)
               //{
               //    client.SendToAll(message, name);
               //}
           }
           catch (Exception e)
           {
               throw new  FaultException(e.Message);
           }
        }
    


    }
    
}
