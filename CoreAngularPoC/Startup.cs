﻿using CoreAngularPoC.Data;
using CoreAngularPoC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAngularPoC
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoreContext>(cfg => cfg.UseSqlServer(_config.GetConnectionString("CoreConnectionString")));
            services.AddTransient<IEmailService, DummyEmailService>();
            services.AddScoped<ICoreRepository, CoreRepository>();
            services.AddMvc();
            services.AddTransient<Seeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            
            app.UseStaticFiles();

            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default", "{controller}/{action}/{id?}", new { controller = "App", Action = "Index" });
            });

            if (env.IsDevelopment())
            {
                using(var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<Seeder>();
                    seeder.Seed();
                }
            }
        }
    }
}
