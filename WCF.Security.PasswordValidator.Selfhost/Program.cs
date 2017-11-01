using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Security.Services;

namespace WCF.Security.PasswordValidator.Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:8084/SecurityService");

            using (ServiceHost host = new ServiceHost(typeof(SecurityService)))
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
