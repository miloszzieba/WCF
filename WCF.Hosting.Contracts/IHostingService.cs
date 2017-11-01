using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Hosting.Contracts
{
    [ServiceContract]
    public interface IHostingService
    {
        [OperationContract]
        string[] GetSelfhostInformation();
        [OperationContract]
        string[] GetSvcUtilInformation();
        [OperationContract]
        string[] GetIISHostingInformation();
        [OperationContract]
        string[] GetWindowsServiceHostingInformation();
    }
}
