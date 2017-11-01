using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Communicator.Services.Infrastructure.Cache
{
    public interface IOperationContextCache
    {
        void AddContext(OperationContext operationContext);
        IEnumerable<OperationContext> GetContexts();
        void Remove(OperationContext context);
    }
}
