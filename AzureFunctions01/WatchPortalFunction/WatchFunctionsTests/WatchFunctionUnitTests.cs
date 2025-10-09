using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;

namespace WatchFunctionsTests
{
    public class WatchFunctionUnitTests
    {
        [Fact]
        public void TestWatchFunctionSuccess()
        {
            var queryStringValue = "abc";
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection
            (
                new System.Collections.Generic.Dictionary<string, StringValues>()
                {
                    { "model", queryStringValue }
                }
            );

            var logger = NullLogger<WatchPortalFunction.WatchInfo>.Instance;
            var response = new WatchPortalFunction.WatchInfo(logger).Run(request);

            Assert.IsAssignableFrom<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            dynamic watchinfo = new { Manufacturer = "abc", CaseType = "Solid", Bezel = "Titanium", Dial = "Roman", CaseFinish = "Silver", Jewels = 15 };
            string watchInfo = $"Watch Details: {watchinfo.Manufacturer}, {watchinfo.CaseType}, {watchinfo.Bezel}, {watchinfo.Dial}, {watchinfo.CaseFinish}, {watchinfo.Jewels}";
            Assert.Equal(watchInfo, result.Value);
        }

        [Fact]
        public void TestWatchFunctionFailureNoQueryString()
        {
            var context = new DefaultHttpContext();
            var request = context.Request;
            var logger = NullLogger<WatchPortalFunction.WatchInfo>.Instance;
            var response = new WatchPortalFunction.WatchInfo(logger).Run(request);

            Assert.IsAssignableFrom<BadRequestObjectResult>(response);
            var result = (BadRequestObjectResult)response;
            Assert.Equal("Please provide a watch model in the query string", result.Value);
        }

        [Fact]
        public void TestWatchFunctionFailureNoModel()
        {
            var queryStringValue = "abc";
            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection
            (
                new System.Collections.Generic.Dictionary<string, StringValues>()
                {
                    { "not-model", queryStringValue }
                }
            );

            var logger = NullLogger<WatchPortalFunction.WatchInfo>.Instance;
            var response = new WatchPortalFunction.WatchInfo(logger).Run(request);

            Assert.IsAssignableFrom<BadRequestObjectResult>(response);
            var result = (BadRequestObjectResult)response;
            Assert.Equal("Please provide a watch model in the query string", result.Value);
        }
    }
}
