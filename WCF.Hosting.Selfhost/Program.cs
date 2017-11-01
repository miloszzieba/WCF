using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Hosting.Services;

namespace WCF.Hosting.Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("net.tcp://localhost:808/HostingService");

            using (ServiceHost host = new ServiceHost(typeof(HostingService)))
            {
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
