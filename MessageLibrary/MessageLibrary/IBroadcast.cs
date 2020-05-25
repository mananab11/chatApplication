using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MessageLibrary
{
    [ServiceContract(CallbackContract =typeof(IBroadcastCallBack))]
    public interface IBroadcast
    {
        [OperationContract(IsOneWay =true)]
        void SendMessage(string msg,List<string> memberId);
        [OperationContract]
        void RegisterClient(string name,string id);
        [OperationContract]
        void UnRegisterClient(string id);

        void BroadcastMessage(string message,string name,List<string> memberId);



    }
    public interface IBroadcastCallBack
    {
        [OperationContract(IsOneWay =true)]
        void SendToAll(string message,string name);
        [OperationContract(IsOneWay=true)]
        void ActiveUsers(int activenumber);
    }
}
