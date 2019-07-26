using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.EF.CodeFirst;
using AnagramGenerator.EF.CodeFirst.Repositories;
using AnagramGenerator.WebApi.Controllers;
using AnagramGenerator.WebApi.Services;
using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnagramGenerator.WebApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Domain = null;
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            services.AddDbContext<WordsDB_CFContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("WordsDB_CF")));

            services.AddScoped<IAppConfig, AppConfig>();
            services.AddScoped<IWordLoader, FileLoader>();

            services.AddScoped<IAnagramsRepository, AnagramsRepository>();
            services.AddScoped<ICachedWordsRepository, CachedWordsRepository>();
            services.AddScoped<IPhrasesRepository, PhrasesRepository>();
            services.AddScoped<IUserLogsRepository, UserLogsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserWordsRepository, UserWordsRepository>();
            services.AddScoped<IWordsRepository, WordsRepository>();

            services.AddScoped<IAnagramsService, AnagramsService>();
            services.AddScoped<ICachedWordsService, CachedWordsService>();
            services.AddScoped<IPhrasesService, PhrasesService>();
            services.AddScoped<IUserLogsService, UserLogsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserWordsService, UserWordsService>();
            services.AddScoped<IWordsService, WordsService>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAnyOriginPolicy");
            app.UseMvc();
        }
    }
}
