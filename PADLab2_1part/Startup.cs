using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PADLab2_1part.Data;
using PADLab2_1part.Infrastructure;
using PADLab2_1part.Services;
using PADLab2_1part.Utils;
using PADLab2_1part.Validation.Extensions;

namespace PADLab2_1part
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
               // var settings = new MongoClientSettings { Server = new MongoServerAddress("localhost", 50003), ConnectionMode = ConnectionMode.Direct };
               // return new MongoClient(settings);

                var uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
                return new MongoClient(uri);
            });

            services.Configure<ApplicationOptions>(Configuration.GetSection("ApplicationOptions"));
            
            services.AddControllers()
                .AddXmlDataContractSerializerFormatters()
                .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
                
            services.AddScoped<IPictureRepo, PictureRepo>();
            services.AddScoped<ILikesRepo, LikesRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPictureService, PictureService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<IPFilterMiddleware>();
            //app.ConfigureExceptionHandler();

            // Hook in the global error-handling middleware
            // app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMiddleware<ErrorHandlingMiddleware>();
            // Register any middleware to report exceptions to a third-party service *after* our ErrorHandlingMiddleware
            //app.UseExcepticon();

            app.UseRouting();

           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
