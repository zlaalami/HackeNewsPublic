using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackerNewsAPI
{
    using HackerNewsAPI.Clients;
    using HackerNewsAPI.Services;

    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddScoped<IHttpClientHandler, HttpClientHandler>();
            services.AddScoped<IStoryHttpClient, StoryHttpClient>();
            //services.AddHttpClient<HttpClientHandler>(o => o.BaseAddress = new Uri(Configuration["HackerNewsApiUri"]));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://hackernews.azurewebsites.net","https://localhost:44376", "http://localhost:61833","localhost:44376", "localhost:61833", "hackernews.azurewebsites.net")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
