using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Security.Contracts;

namespace WCF.Security.Services
{
    public class SecurityService : ISecurityService
    {
        //[PrincipalPermission(SecurityAction.Demand, Role = "Builtin\\Administrators")]
        public string GetProtectedResource()
        {
            var claimsPrincipal = OperationContext.Current.ClaimsPrincipal;
            var claims = ClaimsPrincipal.Current;
            var claimSet = ClaimSet.System;
            var windowsClaimSet = ClaimSet.Windows;
            return "ProtectedResource";
        }
    }
}
