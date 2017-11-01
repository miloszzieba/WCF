using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Security.Contracts;

namespace WCF.Security.Certificates.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<ISecurityService>("wsHttpBinding_ISecurityService");
            var client = channelFactory.CreateChannel();

            try
            {
                var result = client.GetProtectedResource();
                Console.WriteLine("We've managed to get to protected resource");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("We've failed to get to protected resource");
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Press <Enter> to exit application");
            Console.ReadKey();
        }
    }
}
