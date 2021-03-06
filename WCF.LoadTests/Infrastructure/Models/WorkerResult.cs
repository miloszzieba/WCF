﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.LoadTests.Infrastructure.Extensions;

namespace WCF.LoadTests.Infrastructure.Models
{
    public class WorkerResult
    {
        public WorkerResult(int threads, bool threadAffinity, TimeSpan elapsed)
        {
            Threads = threads;
            ThreadAffinity = threadAffinity;
            Elapsed = elapsed;
            Seconds = new Dictionary<int, Second>();
            Histogram = new int[0];
            Exceptions = new Dictionary<Type, int>();
        }

        public int Threads { get; private set; }
        public bool ThreadAffinity { get; private set; }
        public int Pipelining { get; private set; }
        public TimeSpan Elapsed { get; private set; }

        public long Count { get; private set; }
        public long Errors { get; private set; }
        public double RequestsPerSecond { get; private set; }

        public Dictionary<Type, int> Exceptions { get; private set; }
        public Dictionary<int, Second> Seconds { get; set; }

        public double Median { get; private set; }
        public double StdDev { get; private set; }
        public double Min { get; private set; }
        public double Max { get; private set; }
        public int[] Histogram { get; private set; }

        internal void Process(CombinedWorkerThreadResult wtResult)
        {
            Seconds = wtResult.Seconds;
            var items = wtResult.Seconds.Select(r => r.Value).DefaultIfEmpty(new Second(0)).ToList();
            Count = items.Sum(s => s.Count);
            RequestsPerSecond = Count / (Elapsed.TotalMilliseconds / 1000);

            foreach (var exception in items.SelectMany(s => s.Exceptions))
            {
                if (Exceptions.ContainsKey(exception.Key))
                    Exceptions[exception.Key] += exception.Value;
                else
                    Exceptions.Add(exception.Key, exception.Value);
            }

            Errors = Exceptions.Sum(e => e.Value);

            var responseTimes = GetResponseTimes(wtResult.ResponseTimes);
            if (!responseTimes.Any())
                return;

            Median = responseTimes.GetMedian();
            StdDev = responseTimes.GetStdDev();
            Min = responseTimes.First();
            Max = responseTimes.Last();
            Histogram = GenerateHistogram(responseTimes);
        }

        private static float[] GetResponseTimes(List<List<float>> items)
        {
            var result = new float[items.Sum(s => s.Count)];
            var offset = 0;

            for (var i = 0; i < items.Count; i++)
            {
                items[i].CopyTo(result, offset);
                offset += items[i].Count;
                items[i] = null;
            }

            GC.Collect();
            Array.Sort(result);
            return result;
        }

        private int[] GenerateHistogram(float[] responeTimes)
        {
            var splits = 80;
            var result = new int[splits];

            if (responeTimes == null || responeTimes.Length < 2)
                return result;

            var max = responeTimes.Last();
            var min = responeTimes.First();
            var divider = (max - min) / splits;
            var step = min;
            var y = 0;

            for (var i = 0; i < splits; i++)
            {
                var count = 0;
                var stepMax = step + divider;

                if (i + 1 == splits)
                    stepMax = float.MaxValue;

                while (y < responeTimes.Length && responeTimes[y] < stepMax)
                {
                    y++;
                    count++;
                }

                result[i] = count;
                step += divider;
            }

            return result;
        }
    }
}
