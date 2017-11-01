using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.DependencyInjection.Contracts;

namespace WCF.DependencyInjection.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //I'm using SelfHost server right now.
            //If you want to use IIS, please change endpoint address in app.config
            // from "http://localhost:8080/DIService" to "http://localhost:8080/DIService.svc"
            var channelFactory = new ChannelFactory<IDIService>("wsHttpBinding_IDIService");
            var client = channelFactory.CreateChannel();

            Console.WriteLine("I'm requesting collection from inner dependency.");
            Console.WriteLine("Right now, I'm waiting for DIService to initialize.");
            var result = client.GetCollection();
            Console.WriteLine("Oh! There it is!");
            Console.WriteLine("It returned collection with one value:");
            Console.WriteLine(result.First());
            Console.WriteLine();
            Console.WriteLine("Press <Enter> to close application.");
            Console.ReadLine();
        }
    }
}
