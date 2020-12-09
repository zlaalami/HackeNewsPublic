using HackerNewsAPI.Controllers;
using HackerNewsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsAPI.Clients.Tests
{
    [TestClass]
    public class StoryControllerTest
    {
        protected StoryController ControllerUnderTest { get; }
        protected Mock<IStoryHttpClient> StoryHttpClientMock { get; }
        protected Mock<IConfiguration> ConfigurationMock { get; }
        protected Mock<IMemoryCache> MemoryCacheMock { get; }

        public StoryControllerTest()
        {
            StoryHttpClientMock = new Mock<IStoryHttpClient>();
            ConfigurationMock = new Mock<IConfiguration>();
            MemoryCacheMock = new Mock<IMemoryCache>();

            ControllerUnderTest = new StoryController(StoryHttpClientMock.Object, ConfigurationMock.Object, MemoryCacheMock.Object);
            ControllerUnderTest.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [TestMethod]
        public void When_Story_Controller_Index_retuns_success()
        {
            var result = ControllerUnderTest.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void When_API_retuns_success()
        {
            // var Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!"", ""url"": ""www.google.com"", ""time"": 123456789}, { ""id"": 2, ""title"": ""Cool post 2!"", ""url"": ""www.yahoo.com"", ""time"": 987456321}]");
            var results = new List<int> { 1, 2, 3, 4 };
            IEnumerable<int> en = results;
            // Act 
            StoryHttpClientMock.Setup(i => i.GetStories(It.IsAny<string>())).Returns(Task.FromResult(en));

            var fakeUrl = "google.com";
            var t = StoryHttpClientMock.Object.GetStories(fakeUrl);

            // Assert
            Assert.AreEqual(t.Result.ToList().Count, results.Count);
        }
    }
}