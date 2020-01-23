using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetworkManager.Core.Services
{
    public interface ISpeedTestService
    {
        Task<Tuple<HttpResponseMessage, TimeSpan>>[] GetTimeResultTasks(List<string> urls);
        Task<Tuple<HttpResponseMessage, TimeSpan>> GetTimeResultAsync(string url);
        string GetAverageTimeSpan(List<TimeSpan> times);
    }
}
