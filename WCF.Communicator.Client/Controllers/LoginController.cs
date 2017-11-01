using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Communicator.Contracts;

namespace WCF.Communicator.Client.Controllers
{
    public class LoginController
    {
        private readonly DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback> _proxy;

        public LoginController(DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback> proxy)
        {
            this._proxy = proxy;
        }

        public void Register()
        {
            _proxy.Invoke(x => x.Register());
        }
    }
}
