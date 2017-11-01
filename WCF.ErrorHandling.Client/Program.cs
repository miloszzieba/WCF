using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.ErrorHandling.Contracts;

namespace WCF.ErrorHandling.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<IErrorService>("wsHttpBinding_IErrorService");
            var client = channelFactory.CreateChannel();

            Console.WriteLine("Let's throw some exceptions on a server");
            Console.WriteLine();
            try
            {
                client.ThrowExpectedException();
            }
            catch (FaultException<ArgumentNullException> ex)
            {
                Console.WriteLine("Oh! There it is!");
                Console.WriteLine("It returned message:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("and detail message:");
                Console.WriteLine(ex.Detail.Message);
                Console.WriteLine();
            }

            Console.WriteLine("This one is going to be totally unexpected.");
            try
            {
                client.ThrowUnexpectedException();
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Unexpected exception message:");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine("You should also check C:\\Logs folder");
                Console.WriteLine("You can view svclog file with SVCTraceViewer. My is here:");
                Console.WriteLine(@"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7 Tools\SvcTraceViewer.exe");
            }

            Console.ReadLine();
        }
    }
}
