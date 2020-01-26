
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkManager.Core.Services;

namespace NetworkManager.Core.Tests
{
    [TestClass]
    public class SpeedTestServiceTests
    {
        private List<TimeSpan> _times;
        [TestInitialize]
        public void TestInitialize()
        {
            _times = new List<TimeSpan> { new TimeSpan(0, 0, 0, 0, 2), new TimeSpan(0, 0, 0, 0, 4), new TimeSpan(0, 0, 0, 0, 6) };
        }

        [TestMethod]
        public void ShouldGetAverageTimeSpan()
        {
            var speedTestService = new SpeedTestService();
            Assert.AreEqual(4.0, speedTestService.GetAverageTimeSpan(_times));
        }


        [TestMethod]
        public void ShouldNotGetAverageTimeSpanEmptyList()
        {
            var speedTestService = new SpeedTestService();
            Assert.IsNull(speedTestService.GetAverageTimeSpan(new List<TimeSpan>()));
        }
    }
}
