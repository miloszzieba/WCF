﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.LoadTests.Infrastructure.Models
{
    internal class CombinedWorkerThreadResult
    {
        public Dictionary<int, Second> Seconds { get; private set; }
        public List<List<float>> ResponseTimes { get; private set; }
        public TimeSpan Elapsed { get; private set; }

        public CombinedWorkerThreadResult(ConcurrentQueue<WorkerThreadResult> results, TimeSpan elapsed)
        {
            Seconds = new Dictionary<int, Second>();
            ResponseTimes = new List<List<float>>();
            Elapsed = elapsed;

            foreach (var result in results)
            {
                foreach (var second in result.Seconds)
                {
                    ResponseTimes.Add(second.Value.ResponseTimes);
                    second.Value.ClearResponseTimes();

                    if (Seconds.ContainsKey(second.Key))
                        Seconds[second.Key].AddMerged(second.Value);
                    else
                        Seconds.Add(second.Key, second.Value);
                }
            }
        }
    }
}
