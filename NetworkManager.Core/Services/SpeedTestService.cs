

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetworkManager.Core.Services
{
    public class SpeedTestService : ISpeedTestService
    {
        public double GetAverageTimeSpan(List<TimeSpan> times) => times.Select(t => t.TotalMilliseconds).Average();
        public Task<Tuple<HttpResponseMessage, TimeSpan>>[] GetTimeResultTasks(List<string> urls) => urls.Select(url => GetTimeResultAsync(url)).ToArray();

        public async Task<Tuple<HttpResponseMessage, TimeSpan>> GetTimeResultAsync(string url)
        {
            var stopWatch = Stopwatch.StartNew();
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                return new Tuple<HttpResponseMessage, TimeSpan>(result, stopWatch.Elapsed);
            }
        }
    }
}
