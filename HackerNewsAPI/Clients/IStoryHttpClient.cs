using HackerNewsAPI.Models;
using HackerNewsAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsAPI.Clients
{
    public interface IStoryHttpClient
    {
        public Task<IEnumerable<int>> GetStories(string url);

        public Task<List<StoryModel>> GetStoryDetails(string url);

        public Task<StoryModel> GetStory(string url, int storyId);
    }
}
