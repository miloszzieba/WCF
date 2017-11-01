using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.LoadTests.Infrastructure.Models
{
    internal class WorkerThreadResult
    {
        public Dictionary<int, Second> Seconds { get; private set; }

        public WorkerThreadResult()
        {
            Seconds = new Dictionary<int, Second>();
        }

        public void Add(int elapsed, float responsetime, bool trackResponseTime)
        {
            GetItem(elapsed).Add(responsetime, trackResponseTime);
        }

        public void AddError(int elapsed, float responsetime, Exception exception)
        {
            GetItem(elapsed).AddError(responsetime, exception);
        }

        private Second GetItem(int elapsed)
        {
            if (Seconds.ContainsKey(elapsed))
                return Seconds[elapsed];

            var second = new Second(elapsed);
            Seconds.Add(elapsed, second);
            return second;
        }
    }
}
