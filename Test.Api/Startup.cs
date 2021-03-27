using Test.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Test.Api.Interfaces;
using Test.Api.Services;
using Test.Core.Interfaces;
using Test.Core.Services;
using EasyNetQ;
using Microsoft.Extensions.Configuration;

namespace Test.Api
{
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ClientContext>(options => options.UseSqlServer(_configuration.GetConnectionString("ClientDatabase")))
                .AddTransient<IBus>(x=> RabbitHutch.CreateBus(_configuration["RabbitConnectionStrings"]))
                .AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetService<ClientContext>()))
                .AddScoped<IClientService, ClientService>();
            
            services.AddSignalR();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
