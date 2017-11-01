using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Contracts;
using WCF.Communicator.Contracts.Models;
using WCF.Communicator.Services.Infrastructure.Cache;
using WCF.Communicator.Services.Infrastructure.ErrorHandler;

namespace WCF.Communicator.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    //[ErrorBehavior(typeof(ErrorHandler))]
    public class CommunicationService : ICommunicationService
    {
        private IOperationContextCache contextCache;

        public CommunicationService(IOperationContextCache contextCache)
        {
            this.contextCache = contextCache;
        }

        public void Register()
        {
            var context = OperationContext.Current;
            contextCache.AddContext(context);
        }

        public void SendMessage(Message message)
        {
            message.Date = DateTime.Now;

            var currentContext = OperationContext.Current;
            var contexts = contextCache.GetContexts().Where(x => x.SessionId != currentContext.SessionId).ToList();
            foreach (var context in contexts)
            {
                if (context.Channel.State == CommunicationState.Opened)
                {
                    try
                    {
                        var client = context.GetCallbackChannel<ICommunicationServiceCallback>();
                        client.ReceiveMessage(message);
                    }
                    catch
                    {
                        contextCache.Remove(context);
                    }
                }
                else
                {
                    contextCache.Remove(context);
                }

            }
        }
    }
}
