using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Contracts;
using WCF.Communicator.Services;
using WCF.Communicator.Services.Infrastructure.Cache;

namespace WCF.Communicator.SelfHost
{
    class Program
    {
        static void Main()
        {
            var container = BuildContainer();
            var baseAddress = new Uri("net.tcp://localhost:808/CommunicationService");

            using (ServiceHost host = new ServiceHost(typeof(CommunicationService)))
            {
                host.AddDependencyInjectionBehavior<ICommunicationService>(container);

                host.Open();


                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CommunicationService>().As<ICommunicationService>();
            builder.RegisterType<OperationContextCache>().As<IOperationContextCache>().SingleInstance();
            return builder.Build();
        }
    }
}
