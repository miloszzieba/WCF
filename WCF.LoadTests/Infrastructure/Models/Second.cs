using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.LoadTests.Infrastructure.Models
{
    public class Second
    {
        public long Count { get; set; }
        public List<float> ResponseTimes { get; private set; }
        public Dictionary<Type, int> Exceptions { get; private set; }
        public int Elapsed { get; private set; }

        public Second(int elapsed)
        {
            Elapsed = elapsed;
            ResponseTimes = new List<float>();
            Exceptions = new Dictionary<Type, int>();
        }

        internal void ClearResponseTimes()
        {
            ResponseTimes = new List<float>();
        }

        public void Add(float responseTime, bool trackResponseTime)
        {
            Count++;

            if (trackResponseTime)
                ResponseTimes.Add(responseTime);
        }

        public void AddError(float responseTime, Exception exception)
        {
            Count++;
            ResponseTimes.Add(responseTime);

            var exceptionType = exception.GetType();
            if (Exceptions.ContainsKey(exceptionType))
                Exceptions[exceptionType]++;
            else
                Exceptions.Add(exceptionType, 1);
        }

        public void AddMerged(Second second)
        {
            Count += second.Count;

            foreach (var exception in second.Exceptions)
            {
                if (Exceptions.ContainsKey(exception.Key))
                    Exceptions[exception.Key] += exception.Value;
                else
                    Exceptions.Add(exception.Key, exception.Value);
            }
        }
    }
}
