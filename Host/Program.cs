using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using MessageLibrary;
using System.ServiceModel.Description;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Broadcast) );
            
            try
            {
                host.Open();
                DisplayEndpoints(host);
                Console.ReadLine();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                host.Abort();
            }
        }

        private static void DisplayEndpoints(ServiceHost host)
        {
            foreach(ServiceEndpoint collection in host.Description.Endpoints)
            {
                Console.WriteLine("running {0} at  {1} ",collection.Name,collection.Address);

            }
        }
    }
}
