using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.ErrorHandling.Contracts;
using WCF.ErrorHandling.Service.Infrastructure.ErrorHandler;

namespace WCF.ErrorHandling.Service
{
    [ErrorBehavior(typeof(ErrorHandler))]
    public class ErrorService : IErrorService
    {
        public void ThrowExpectedException()
        {
            throw new FaultException<ArgumentNullException>(new ArgumentNullException("paramName"));
        }

        public void ThrowUnexpectedException()
        {
            throw new ArgumentOutOfRangeException("index");
        }
    }
}
