using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client1.MessageServiceReference1;
using System.ServiceModel.Description;
using System.ServiceModel;
namespace client1
{
    public class MessagelibraryServiceCallback : IBroadcastCallback
    {
        public static int activeClientsCounter;
        
        public void SendToAll(string str,string name)
        {
            Console.WriteLine("{0} : {1} ",name,str);
        }
        public void ActiveUsers(int activeClientsCount)
        {

            activeClientsCounter = activeClientsCount;
        }
       
       
        
      
    }
    class Program
    {
        static void Main(string[] args)
        {
            MessagelibraryServiceCallback objects = new MessagelibraryServiceCallback();
            InstanceContext context = new InstanceContext(objects);
            BroadcastClient client = new BroadcastClient(context);
            try
            {
                List<string> memberId;
                Console.WriteLine("enter ur name");
                string name = Console.ReadLine();
                Console.WriteLine("enter id");
                string id = Console.ReadLine();
                client.RegisterClient( name,id);
                Console.WriteLine("welcome {0} ",name);
                Console.WriteLine("Enter 'group' for Group,'peer' for peer to peer chat");
                Console.WriteLine("count for active users,exit for closing ");

                string command = Console.ReadLine();
                    switch (command)
                    {
                        case "exit":                      
                            break;
                   
                        case "peer":

                        bool flag = true;
                        Console.WriteLine("enter id to chat with");
                        memberId = new List<string>();
                        memberId.Add(Console.ReadLine());
                        Console.WriteLine("enter messages now and 'exit' to close");
                        while (flag)
                        {
                            string messagetosend = Console.ReadLine();
                            switch (messagetosend)
                            {
                                case "exit":
                                    flag = false;
                                    break;
                                case "count":
                                    Console.WriteLine(MessagelibraryServiceCallback.activeClientsCounter);
                                    break;
                                default:
                                    client.SendMessage(messagetosend, memberId);
                                    break;
                            }

                        }

                        break;
                       
                        case "group":
                            Console.WriteLine("enter id's to connect with and'finish' to end adding");
                            bool idFlag = true;
                            string enterIdsforGroup;
                            memberId = new List<string>();
                            while (idFlag)
                             {
                                enterIdsforGroup = Console.ReadLine();
                                if(enterIdsforGroup!="finish")
                                   {
                                      memberId.Add(enterIdsforGroup);
                                    }
                                else { idFlag = false; }
                              }
                           
                            Console.WriteLine("enter messages now or 'exit' to close");
                            bool flags = true;
                            
                            while (flags)
                                {
                                  string commands = Console.ReadLine();
                                  switch (commands)
                                    {
                                     case "exit":
                                      flags = false;
                                      break;
                                    case "count":
                                     Console.WriteLine(MessagelibraryServiceCallback.activeClientsCounter);
                                     break;
                                default:
                                     client.SendMessage(commands, memberId);
                                     break;
                                    }
                                }
                            break;
                        default:
                        Console.WriteLine("invalid command");
                            break;
                    }            
                client.UnRegisterClient(id);
                client.Close();
                Console.WriteLine("closed success");
                Console.ReadLine();
            }
            catch(FaultException fe)
            {
                Console.WriteLine(fe.Message);
                client.Abort();
            }
            catch(CommunicationException ce)
            {
                Console.WriteLine(ce.Message);
                client.Abort();
            }
            catch (Exception )
            {
                client.Abort();
                throw;
            }
           



        }
        
    }
}
