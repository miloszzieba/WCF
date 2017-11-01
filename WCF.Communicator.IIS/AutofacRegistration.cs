using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCF.Communicator.Services;
using WCF.Communicator.Services.Infrastructure.Cache;

namespace WCF.Communicator.IIS
{
    public class AutofacRegistration
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CommunicationService>();
            builder.RegisterType<OperationContextCache>().As<IOperationContextCache>().SingleInstance();
            var container = builder.Build();

            AutofacHostFactory.Container = container;
        }
    }
}