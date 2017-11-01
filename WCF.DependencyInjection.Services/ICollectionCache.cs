using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.DependencyInjection.Services
{
    public interface ICollectionCache
    {
        IEnumerable<string> GetCollectionCache();
    }
}
