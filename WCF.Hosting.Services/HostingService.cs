using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Hosting.Contracts;

namespace WCF.Hosting.Services
{
    public class HostingService : IHostingService
    {
        public string[] GetIISHostingInformation()
        {
            return new string[]
           {
                "IIS Server:",
                "1. You have to enable IIS Features.",
                "2. In IIS setup application pool",
                "3. Change its identity or give access rights to your application directory to IIS_IUSRS",
                "4. Setup new Website under this pool with http binding (even if it's net.tcp app)",
                "5. If it's net.tcp app, add net.tcp binding",
                "   and website Advanced Settings -> Enabled Protocols set to 'http, net.tcp'",
                "6. Website directory should be set to directory with Web.config, not bin folder",
                "7. In Web.config you should have activate your Service like this:",
                "   <serviceHostingEnvironment multipleSiteBindingsEnabled=\"true\">",
                "       <serviceActivations>",
                "           <add relativeAddress=\"HostingService.svc\"",
                "                service=\"WCF.Hosting.Services.HostingService, WCF.Hosting.Services\" />",
                "       </serviceActivations>",
                "   </serviceHostingEnvironment>",
                "8. You should not have baseAddresses section",
                "9. Your endpoint adress should be set to \"\""
           };
        }

        public string[] GetSelfhostInformation()
        {
            return new string[]
            {
                "SELFHOST Server:",
                "Just look into app.config and Program.Main() of WCF.Hosting.Selfhost.",
                "It's pretty simple"
            };
        }

        public string[] GetSvcUtilInformation()
        {
            return new string[]
           {
                "SVCUTIL Client:",
                "If you want to use svcutil, you should specify addiditonal IMetadataExchange endpoint on server like this:",
                "<endpoint address=\"mex\"",
                " binding=\"mexHttpBinding\"",
                " contract=\"IMetadataExchange\"/>",
                "and add service behavior like this:",
                "<behaviors>",
                "    <serviceBehaviors>",
                "        <behavior>",
                "            <serviceMetadata httpsGetEnabled=\"True\"/>",
                "        </behavior>",
                "    </serviceBehaviors>",
                "</behaviors>",
                "",
                "To use svcutil, you should be in cmd as Administrator",
                "and in project rood directory execute:",
                "svcutil.exe /language:cs /out:HostingServiceProxy.cs /config:app.config http://localhost:8080/HostingService",
                "",
                "Then include HostingServiceProxy.cs in project",
                "and you are ready to use HostingServiceClient."
           };
        }

        public string[] GetWindowsServiceHostingInformation()
        {
            return new string[]
           {
                "WINDOWSSERVICE Server:",
                "Just look at WCF.Hosting.WindowsService",
                "It does almost the same as WCF.Hosting.Selfhost"
           };
        }
    }
}
