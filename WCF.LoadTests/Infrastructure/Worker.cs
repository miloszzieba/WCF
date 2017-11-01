using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCF.LoadTests.Infrastructure.Helpers;
using WCF.LoadTests.Infrastructure.Models;

namespace WCF.LoadTests.Infrastructure
{
    public static class Worker
    {
        public static WorkerResult Run(Action action, int threads, bool threadAffinity, TimeSpan duration, int? count, CancellationToken cancellationToken)
        {
            var combinedWorkerThreadResult = QueueWorkerThreads(action, threads, threadAffinity, duration, count, cancellationToken);
            var result = new WorkerResult(threads, threadAffinity, combinedWorkerThreadResult.Elapsed);
            result.Process(combinedWorkerThreadResult);
            return result;
        }

        private static CombinedWorkerThreadResult QueueWorkerThreads(Action action, int threads, bool threadAffinity, TimeSpan duration, int? count, CancellationToken cancellationToken)
        {
            var results = new ConcurrentQueue<WorkerThreadResult>();
            var events = new List<ManualResetEventSlim>();
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < threads; i++)
            {
                var resetEvent = new ManualResetEventSlim(false);

                ThreadHelper.QueueThread(i, threadAffinity, (threadIndex) =>
                {
                    DoWork(action, duration, count, results, sw, cancellationToken, resetEvent, threadIndex);
                });

                events.Add(resetEvent);
            }

            for (var i = 0; i < events.Count; i += 50)
            {
                var group = events.Skip(i).Take(50).Select(r => r.WaitHandle).ToArray();
                WaitHandle.WaitAll(group);
            }
            sw.Stop();

            return new CombinedWorkerThreadResult(results, sw.Elapsed);
        }

        private static void DoWork(Action action, TimeSpan duration, int? count, ConcurrentQueue<WorkerThreadResult> results, Stopwatch sw, CancellationToken cancellationToken, ManualResetEventSlim resetEvent, int workerIndex)
        {
            var result = new WorkerThreadResult();
            var sw2 = new Stopwatch();
            var sw3 = new Stopwatch();
            var current = 0;

            // To save memory we only track response times from the first 20 workers
            var trackResponseTime = workerIndex < 20;

            while (!cancellationToken.IsCancellationRequested && duration.TotalMilliseconds > sw.Elapsed.TotalMilliseconds && (!count.HasValue || current < count.Value))
            {
                current++;
                try
                {
                    sw2.Restart();

                    action.Invoke();

                    result.Add((int)(sw.ElapsedTicks / Stopwatch.Frequency), (float)sw2.ElapsedTicks / Stopwatch.Frequency * 1000, trackResponseTime);
                }
                catch (Exception ex)
                {
                    result.AddError((int)(sw.ElapsedTicks / Stopwatch.Frequency), (float)sw2.ElapsedTicks / Stopwatch.Frequency * 1000, ex);
                }
            }

            results.Enqueue(result);
            resetEvent.Set();
        }
    }
}
