using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Client.Infrastructure;
using WCF.Communicator.Contracts.Infrastructure;

namespace WCF.Communicator.Client
{
    public class DuplexServiceProxy<T, TCallback> where T : class, IService where TCallback : class, ICallbackService
    {
        private DuplexChannelFactory<T> _duplexChannelFactory;
        private Pool<T> _proxyPool;

        public DuplexServiceProxy(TCallback serviceCallback, string endpointConfigurationName)
        {
            var instanceContext = new InstanceContext(serviceCallback);
            _duplexChannelFactory = new DuplexChannelFactory<T>(instanceContext, endpointConfigurationName);
            _proxyPool = new Pool<T>(100);
        }

        public void Invoke(Action<T> invokeMethod)
        {
            var proxy = GetProxy();

            try
            {
                invokeMethod(proxy);
            }
            catch (Exception)
            {
                //You can handle errors here
                ((IClientChannel)proxy).Abort();
                throw;
            }

            CloseProxy(proxy);
        }

        private T GetProxy()
        {
            T proxy = null;

            // Trying to take the proxy from the pool
            lock (_proxyPool)
            {
                proxy = _proxyPool.Take();
            }

            // Remove bad proxies (Faulted etc). Note that checking the proxy state has
            // some performance cost here as it accesses the Transparent proxy of the
            // channel.
            if (proxy != null && ((IClientChannel)proxy).State != CommunicationState.Opened)
            {
                ((IClientChannel)proxy).Abort();
                proxy = null;
            }

            // Create the proxy if not found from the pool.
            if (proxy == null)
                proxy = _duplexChannelFactory.CreateChannel();

            return proxy;
        }

        private void CloseProxy(T proxy)
        {
            // Trying to put the proxy back to the pool
            bool shouldClose = false;

            lock (_proxyPool)
            {
                shouldClose = !_proxyPool.Return(proxy);
            }

            // Close the proxy if it's not put back into pool
            if (shouldClose)
            {
                try
                {
                    ((IClientChannel)proxy).Close();
                }

                catch (Exception)
                {
                    ((IClientChannel)proxy).Abort();
                    throw;
                }
            }
        }
    }
}
