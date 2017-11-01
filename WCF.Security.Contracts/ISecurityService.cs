using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Security.Contracts
{
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        string GetProtectedResource();
    }
}
