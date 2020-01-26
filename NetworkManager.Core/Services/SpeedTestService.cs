

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetworkManager.Core.Services
{
    /// <summary>
    /// Speed Test service class
    /// Contains all methods for performig speed test functionality
    /// </summary>
    public class SpeedTestService : ISpeedTestService
    {
        /// <summary>
        /// Provided with list of time spans Selects milliseconds and returns average values
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public double GetAverageTimeSpan(List<TimeSpan> times) => times.Select(t => t.TotalMilliseconds).Average();
        /// <summary>
        ///  Provided with list of urls executes GetTimeResult method and combines time results into array
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public Task<Tuple<HttpResponseMessage, TimeSpan>>[] GetTimeResultTasks(List<string> urls) => urls.Select(url => GetTimeResultAsync(url)).ToArray();
        /// <summary>
        /// Provided with url starts stop watch sends request to provided url and returs tuple that consists of result and elapsed time
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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
