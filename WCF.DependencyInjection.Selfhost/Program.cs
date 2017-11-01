using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.DependencyInjection.Contracts;
using WCF.DependencyInjection.Services;

namespace WCF.DependencyInjection.Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();
            using (ServiceHost host = new ServiceHost(typeof(DIService)))
            {
                host.AddDependencyInjectionBehavior<IDIService>(container);
                host.Open();

                var uri = new Uri("http://localhost:8080/DIService");
                Console.WriteLine("The service is ready at {0}", uri);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DIService>().As<IDIService>();
            builder.RegisterType<ListCollectionCache>().As<ICollectionCache>().SingleInstance();
            return builder.Build();
        }
    }
}
