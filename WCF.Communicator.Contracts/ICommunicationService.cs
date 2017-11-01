using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Contracts.Infrastructure;
using WCF.Communicator.Contracts.Models;

namespace WCF.Communicator.Contracts
{
    [ServiceContract(CallbackContract = typeof(ICommunicationServiceCallback))]
    public interface ICommunicationService : IService
    {
        [OperationContract]
        void Register();

        [OperationContract]
        [ServiceKnownType(typeof(AuthorMessage))]
        void SendMessage(Message message);
    }


    public interface ICommunicationServiceCallback : ICallbackService
    {
        [OperationContract(IsOneWay = true)]
        [ServiceKnownType(typeof(AuthorMessage))]
        void ReceiveMessage(Message message);
    }
}
