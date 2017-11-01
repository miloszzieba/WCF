using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Hosting.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HostingServiceClient();
            var response = client.GetIISHostingInformation();
            foreach (var line in response)
                Console.WriteLine(line);

            Console.WriteLine();

            response = client.GetSelfhostInformation();
            foreach (var line in response)
                Console.WriteLine(line);

            Console.WriteLine();

            response = client.GetSvcUtilInformation();
            foreach (var line in response)
                Console.WriteLine(line);

            Console.WriteLine();

            response = client.GetWindowsServiceHostingInformation();
            foreach (var line in response)
                Console.WriteLine(line);

            Console.ReadLine();
        }


    }
}
