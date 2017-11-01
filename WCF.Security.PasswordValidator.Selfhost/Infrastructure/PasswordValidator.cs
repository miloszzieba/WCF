using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Security.PasswordValidator.Selfhost.Infrastructure
{
    public class PasswordValidator : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            if (userName == "TEST" && password == "TEST")
            {
                return;
            }

            throw new FaultException("Unknown Username or Incorrect Password");

            
        }
    }
}
