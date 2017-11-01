using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Contracts;
using WCF.Communicator.Contracts.Models;

namespace WCF.Communicator.Client.Controllers
{
    public class ChatController
    {
        private string _name;
        private readonly DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback> _proxy;

        public ChatController(DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback> proxy)
        {
            this._proxy = proxy;
        }

        public void StartChat()
        {
            Console.WriteLine("Welcome to SAGES ChatClient!");
            Console.WriteLine("Please state your name:");
            _name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Welcome to our chat!");
            Console.WriteLine("If you want to leave, just write exit.");
        }

        public void OpenChatWindow()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "exit")
                    break;

                _proxy.Invoke(x => x.SendMessage(new AuthorMessage()
                {
                    Text = line,
                    Author = this._name
                }));
            }
        }
    }
}
