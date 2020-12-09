using HackerNewsAPI.Clients;
using HackerNewsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Threading;

namespace HackerNewsAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryHttpClient _storyHttpClient;
        private readonly IConfiguration _configuration;
        private IMemoryCache _memoryCache;

        public StoryController(IStoryHttpClient storyHttpClient, IConfiguration configuration, IMemoryCache memoryCache)
        {
            this._storyHttpClient = storyHttpClient;
            this._configuration = configuration;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<List<StoryModel>> Index()
        {
            Func<StoryModel, bool> storiesList = r => r.Url != null;

            var cacheKey = "storiesList";
            List<StoryModel> stories = null;

            if (_memoryCache.TryGetValue(cacheKey, out stories))
            {
                return stories.Where(storiesList).ToList();
            }

            try
            {
                stories = await _storyHttpClient.GetStoryDetails(_configuration["HackerNewsApiUri"]);

                var cacheExpirationOptions = new MemoryCacheEntryOptions();
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(int.Parse(_configuration["CacheExpirationInMinutes"]));
                cacheExpirationOptions.Priority = CacheItemPriority.Normal;

                _memoryCache.Set(cacheKey, stories.Where(s=>s.Url!=null), cacheExpirationOptions);

                return stories.Where(s=>s.Url!=null).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
