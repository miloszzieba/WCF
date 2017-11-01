using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF.Communicator.Client;
using WCF.Communicator.Client.Controllers;
using WCF.Communicator.Contracts;

namespace WCF.Tests
{
    //Integration tests are end-to-end.
    //They need working servers (and sometimes databases) to test logic of whole application
    [TestClass]
    public class WCF_IntegrationTests
    {
        [TestMethod]
        public void LoginController_Register_DoesntThrowException()
        {
            //ARRANGE
            var proxy = new DuplexServiceProxy<ICommunicationService, ICommunicationServiceCallback>(new CommunicationServiceCallback(), "netTcpBinding_ICommunicationService");
            var loginController = new LoginController(proxy);

            //ACT
            loginController.Register();

            //ASSERT (Actually here, there is nothing to assert)
            Assert.IsTrue(true);
        }
    }
}
