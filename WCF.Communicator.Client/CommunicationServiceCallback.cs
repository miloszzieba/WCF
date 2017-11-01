using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Contracts;
using WCF.Communicator.Contracts.Models;

namespace WCF.Communicator.Client
{
    public class CommunicationServiceCallback : ICommunicationServiceCallback
    {
        public void ReceiveMessage(Message message)
        {
            if (message as AuthorMessage != null)
                Console.WriteLine(message.Date.ToShortTimeString() + " - " + (message as AuthorMessage).Author + ": " + message.Text);
            else
                Console.WriteLine(message.Date.ToShortTimeString() + ": " + message.Text);
        }
    }
}
