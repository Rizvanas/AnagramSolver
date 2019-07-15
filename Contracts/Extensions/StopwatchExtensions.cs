using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Contracts.Extensions
{
    public static class StopwatchExtensions
    {
        public static long Time(this Stopwatch stopwatch, Action action)
        {
            stopwatch.Restart();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds * 1000;
        }
    }
}
