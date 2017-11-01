using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Communicator.Services.Infrastructure.Cache
{
    public class OperationContextCache : IOperationContextCache
    {
        private SynchronizedCollection<OperationContext> contexts = new SynchronizedCollection<OperationContext>();

        public void AddContext(OperationContext operationContext)
        {
            this.contexts.Add(operationContext);
        }

        public IEnumerable<OperationContext> GetContexts()
        {
            return this.contexts;
        }

        public void Remove(OperationContext context)
        {
            this.contexts.Remove(context);
        }
    }
}
