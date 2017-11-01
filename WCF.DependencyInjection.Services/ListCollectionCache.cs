using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCF.DependencyInjection.Services
{
    public class ListCollectionCache : ICollectionCache
    {
        private List<string> _collection = new List<string>() { "TEST" };

        public ListCollectionCache()
        {
            Thread.Sleep(200);
        }

        public IEnumerable<string> GetCollectionCache()
        {
            return this._collection;
        }
    }
}
