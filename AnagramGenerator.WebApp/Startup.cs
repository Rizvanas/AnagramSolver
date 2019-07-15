using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementation;
using Contracts;
using Contracts.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AnagramGenerator.EF.DatabaseFirst;
using AnagramGenerator.EF.DatabaseFirst.Repositories;

namespace AnagramGenerator.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Domain = null;
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            services.AddDbContext<WordsDBContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("WordsDB")));

            services.AddScoped<IUserLogRepository, UserLogRepository>();   
            services.AddScoped<IAppConfig, AppConfig>();
            services.AddScoped<IWordLoader, FileLoader>();
            services.AddScoped<IAnagramsRepository, AnagramsRepository>();
            services.AddScoped<ICachedWordsRepository, CachedWordsRepository>();
            services.AddScoped<IPhrasesRepository, PhrasesRepository>();
            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<IWordsRepository, WordsRepository>();
            services.AddScoped<IAnagramSolver, AnagramSolver>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
