using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.Communicator.IIS.App_Code
{
    public class AppStart
    {
        public static void AppInitialize()
        {
            AutofacRegistration.Register();
        }
    }
}