using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Client.Controllers;
using WCF.Communicator.Contracts;
using WCF.Communicator.Contracts.Models;

namespace WCF.Communicator.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback>(new CommunicationServiceCallback(), "netTcpBinding_ICommunicationService");
            var loginController = new LoginController(proxy);
            var chatController = new ChatController(proxy);

            loginController.Register();

            chatController.StartChat();
            chatController.OpenChatWindow();

            Console.ReadKey();
        }
    }
}
