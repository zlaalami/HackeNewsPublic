using HackerNewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsAPI.Services
{
    public interface IHttpClientHandler
    {
        Task<TResult> GetAsync<TResult>(string requestUri);

        HttpClient HttpClient { get; set; }
    }
}
