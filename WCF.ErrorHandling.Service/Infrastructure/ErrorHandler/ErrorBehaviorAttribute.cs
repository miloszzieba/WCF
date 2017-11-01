using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ErrorHandling.Service.Infrastructure.ErrorHandler
{
    public sealed class ErrorBehaviorAttribute : Attribute, IServiceBehavior
    {
        private Type errorHandlerType;

        public ErrorBehaviorAttribute(Type errorHandlerType)
        {
            this.errorHandlerType = errorHandlerType;
        }

        public Type ErrorHandlerType
        {
            get { return this.errorHandlerType; }
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler;

            try
            {
                errorHandler = (IErrorHandler)Activator.CreateInstance(errorHandlerType);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (MissingMethodException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehavior must have a public empty constructor");
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (InvalidCastException e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                throw new ArgumentException("The errorHandlerType specified in the errorBehavior must implement IErrorHandler");
            }

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }


        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
