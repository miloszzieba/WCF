using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCF.DependencyInjection.Services
{
    public class SynchronizedCollectionCache : ICollectionCache
    {
        private SynchronizedCollection<string> _collection = new SynchronizedCollection<string>() { "TEST" };

        public SynchronizedCollectionCache()
        {
            Thread.Sleep(200);
        }

        public IEnumerable<string> GetCollectionCache()
        {
            return this._collection;
        }
    }
}
