using DataBaseAccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yacht_harbour.Data;
using Repository.Repositories;
using Repository.Services;
using yacht_harbour.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;



namespace yacht_harbour
{

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
           
            services.AddServerSideBlazor().AddHubOptions(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
                options.HandshakeTimeout = TimeSpan.FromSeconds(30);
            });

            services.AddDbContextFactory<yacht_harbour_ver7Context>(options => options.UseMySql(Configuration.GetConnectionString("Default"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.2-mariadb")));
            services.AddDbContext<yacht_harbour_ver7Context>(opt => opt.UseMySql(Configuration.GetConnectionString("Default"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.2-mariadb")));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<AccountService>();

            services.AddScoped<AccountManagmentService>();
            services.AddScoped<CalendarService>();

            services.AddScoped<IFunctionRepository, FunctionRepository>();
            services.AddScoped<FunctionService>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<StatusService>();
            services.AddScoped<IYachtRepository, YachtRepository>();
            services.AddScoped<YachtService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<OrderService>();
            services.AddScoped<IPowerRepository, PowerRepository>();
            services.AddScoped<PowerService>();

            services.AddBlazoredSessionStorage();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

