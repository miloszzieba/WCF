using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.DependencyInjection.Contracts;

namespace WCF.DependencyInjection.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DIService : IDIService
    {
        private ICollectionCache _collectionCache;

        public DIService(ICollectionCache collectionCache)
        {
            this._collectionCache = collectionCache;
        }

        public IEnumerable<string> GetCollection()
        {
            return this._collectionCache.GetCollectionCache();
        }
    }
}
