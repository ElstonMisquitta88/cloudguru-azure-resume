using Microsoft.Extensions.Logging;
using Xunit;
using System.Net.Http;

namespace tests
{
    public class AzFunctionTest
    {
         private readonly ILogger logger = TestFactory.CreateLogger();
        [Fact]
        public void ReturnCounterCount()
        {
            var counter = new CustomCounter.Counter();
            counter.Id = "One";
            counter.Count = 15;

             var request = TestFactory.CreateHttpRequest();
             var response = (HttpResponseMessage) CustomCounter.ResumeCounterTrigger.Run(request, counter, out counter, logger);
             Assert.Equal(16, counter.Count);
        }
    }
}
