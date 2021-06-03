using MarvelWebApp.Configuration;
using MarvelWebApp.Data;
using MarvelWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

namespace MarvelWebApp
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
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            // Configure options.
            services.Configure<MarvelApiSettings>(Configuration.GetSection(nameof(MarvelApiSettings)));
            // Configure services.
            var useInMemoryData = Configuration.GetValue<bool>("UseInMemoryData");
            if (useInMemoryData)
            {
                services.AddTransient<IHeroService, InMemoryHeroService>();
            }
            else
            {
                services.AddHttpClient<IHeroService, MarvelHeroService>()
                        .SetHandlerLifetime(TimeSpan.FromHours(1))
                        .ConfigureHttpClient((serviceProvider, httpClient) =>
                        {
                            var apiSettings = serviceProvider.GetService<IOptions<MarvelApiSettings>>();
                            httpClient.BaseAddress = new Uri(apiSettings.Value.BaseAddress);
                        });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
