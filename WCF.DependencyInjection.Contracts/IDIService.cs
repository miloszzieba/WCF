using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.DependencyInjection.Contracts
{
    [ServiceContract]
    public interface IDIService
    {
        [OperationContract]
        IEnumerable<string> GetCollection();
    }
}
