using HackerNewsAPI.Models;
using HackerNewsAPI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsAPI.Clients
{
    public class StoryHttpClient: IStoryHttpClient
    {
        private readonly IHttpClientHandler _httpClientHandler;
        private readonly IConfiguration _configuration;

        public StoryHttpClient(IHttpClientHandler httpClientHandler, IConfiguration configuration)
        {
            _httpClientHandler = httpClientHandler;
            this._configuration = configuration;
        }
        public async Task<IEnumerable<int>> GetStories(string url)
        {
            var stringResult = await this._httpClientHandler.GetAsync<HttpResponseMessage>(url + _configuration["HackerNewsStories"]).ConfigureAwait(false);
            var storiesDTOs = JsonConvert.DeserializeObject<List<int>>(stringResult.Content.ReadAsStringAsync().Result);
            return storiesDTOs;
        }

        public async Task<List<StoryModel>> GetStoryDetails(string url)
        {
            var result = new List<StoryModel>();
            try
            {
                var storiesIds = await GetStories(url);
                if (storiesIds != null)
                {
                    foreach (var storyId in storiesIds)
                    {
                        var story = GetStory(url,storyId);
                        result.Add(story.Result);
                    }
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<StoryModel> GetStory(string url, int storyId)
        {
            var stringResult = await this._httpClientHandler.GetAsync<HttpResponseMessage>(url+"item/" + storyId + ".json");
            var story = JsonConvert.DeserializeObject<StoryModel>(stringResult.Content.ReadAsStringAsync().Result);
            return story;
            
        }
    }
}
