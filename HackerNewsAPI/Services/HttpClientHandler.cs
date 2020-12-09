﻿using HackerNewsAPI.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackerNewsAPI.Clients
{
    public class HttpClientHandler : IHttpClientHandler
    {
        public HttpClient HttpClient { get; set; }

        public async Task<TResult> GetAsync<TResult>(string requestUri)
        {
            TResult objResult = default(TResult);

            using (var client = this.GetHttpClient())
            {
                var response = await client.GetAsync(requestUri);
                
                    if (TryParse<TResult>(response, out objResult))
                    {
                        return objResult;
                    }

                    using (HttpContent content = response.Content)
                    {
                        throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
                    }
            }
        }
        private HttpClient GetHttpClient()
        {
            if (HttpClient == null)//While mocking we set httpclient object to bypass actual result.
            {
                var _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return _httpClient;
            }
            return HttpClient;
        }

        private bool TryParse<TResult>(HttpResponseMessage response, out TResult t)
        {
            if (typeof(TResult).IsAssignableFrom(typeof(HttpResponseMessage)))
            {
                t = (TResult)Convert.ChangeType(response, typeof(TResult));
                return true;
            }

            if (response.IsSuccessStatusCode)
            {
                t = (TResult)Convert.ChangeType(response.Content.ReadAsStringAsync().Result, typeof(TResult));
                return true;
            }

            t = default(TResult);
            return false;
        }
    }
}
