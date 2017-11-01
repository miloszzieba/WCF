using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCF.Communicator.Client;
using WCF.Communicator.Client.Controllers;
using WCF.Communicator.Contracts;
using WCF.LoadTests.Infrastructure;
using WCF.LoadTests.Infrastructure.Helpers;

namespace WCF.LoadTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var threads = 50;
            var time = new TimeSpan(0, 1, 0);
            int? count = null;


            Console.WriteLine("Welcome to our TestLoadTool!");
            //Powód występowania błędu:
            //System osiągnął limit przypisany przepustnicy „MaxConcurrentSessions”. Jako limit przypisany tej przepustnicy określono wartość 800.
            //Po osiągnięciu limitu WCF przestał przyjmować zapytania. 
            //Zapominaliśmy zamykać sesję.
            //Zaimplementowałem to tak, jak powinno wyglądać w praktyce.
            var proxy = new DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback>(new CommunicationServiceCallback(), "netTcpBinding_ICommunicationService");

            //Warming up channelFactory
            proxy.Invoke(x => x.Register());

            var action = new Action(() =>
            {
                proxy.Invoke(x => x.Register());
            });

            Console.WriteLine("I'm starting tests!");
            Console.WriteLine("Right now time is: {0}", DateTime.Now.ToLongTimeString());
            Console.WriteLine("Wait to {0} for test results.", DateTime.Now.Add(time).ToLongTimeString());

            var result = Worker.Run(action, threads, true, time, count, new CancellationToken());
            Console.WriteLine("Łączna liczba wykonanych akcji: {0}", result.Count);
            Console.WriteLine("Czas testu: {0}", result.Elapsed);
            Console.WriteLine("Ilość błędów: {0}", result.Errors);
            Console.WriteLine("Maksymalny czas akcji: {0}", result.Max);
            Console.WriteLine("Średni czas akcji: {0}", result.Median);
            Console.WriteLine("Minimalny czas akcji: {0}", result.Min);
            Console.WriteLine("Odchylenie standardowe czasu: {0}", result.StdDev);
            Console.WriteLine("Ilość akcji na sekundę: {0}", result.RequestsPerSecond);
            Console.WriteLine("Ilość wątków: {0}", result.Threads);
            Console.WriteLine();
            Console.WriteLine(ConsoleHelper.GetAsciiHistogram(result));
            Console.ReadKey();

            //What else could we add to our tool?

            //For example passing list of Dictionary of actions and their names to Worker.Run.
            //Then we could measure complicated scenarios, but with different methods after each other

            //Or passing arguments, so we didnt have to recompilate when change'ing threads, time or count
        }
    }
}
