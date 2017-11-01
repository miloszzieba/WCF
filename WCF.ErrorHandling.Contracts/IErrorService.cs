using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ErrorHandling.Contracts
{
    [ServiceContract]
    public interface IErrorService
    {
        [OperationContract]
        [FaultContract(typeof(ArgumentNullException))]
        void ThrowExpectedException();

        [OperationContract]
        void ThrowUnexpectedException();
    }
}
