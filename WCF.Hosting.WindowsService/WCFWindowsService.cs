using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WCF.Hosting.Services;

namespace WCF.Hosting.WindowsService
{
    public class WCFWindowsService : ServiceBase
    {

        public ServiceHost serviceHost = null;

        public static void Main()
        {
            ServiceBase.Run(new WCFWindowsService());
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(HostingService));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            serviceHost.Close();
        }
    }
}
