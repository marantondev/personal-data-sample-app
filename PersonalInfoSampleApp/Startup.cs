using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PersonalInfoSampleApp.Persistence;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using PersonalInfoSampleApp.Pages.Form.Handlers;
using PersonalInfoSampleApp.Repositories;

namespace PersonalInfoSampleApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(p => p.ResourcesPath = "Resources");


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(p =>
            {
                var cultures = new List<CultureInfo>()
                {
                    new CultureInfo("pl-PL")
                };

                p.DefaultRequestCulture = new RequestCulture("pl-PL");
                p.SupportedCultures = cultures;
                p.SupportedUICultures = cultures;
            });

            services.AddTransient<IPersonalInfoRepository, PersonalInfoRepository>();
            services.AddTransient<ISubmitPersonalInfoCommandHandler, SubmitPersonalInfoCommandHandler>();
            services.AddTransient<IGetCityListQueryHandler, GetCityListQueryHandler>();

            services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PersonalInfoSampleAppContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRequestLocalization();

            app.UseMvc();
        }
    }
}
