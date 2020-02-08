using FurnitureShopApp.BLL.Interfaces;
using FurnitureShopApp.BLL.Services;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurnitureShopApp
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

            services.AddScoped<ICheckDetailsService, CheckDetailsService>();
            services.AddScoped<ICompanyDeveloperRepository, CompanyDeveloperRepository>();
            services.AddScoped<ITypesRepository, TypesRepository>();
            services.AddScoped<ISubtypeRepository, SubtypeRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IFurnitureShopRepository, FurnitureShopRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeePositionRepository, EmployeePositionRepository>();
            services.AddScoped<IFurnitureRepository, FurnitureRepository>();
            services.AddScoped<IFurnitureShopUnitOfWork, FurnitureShopUnitOfWork>();
            services.AddScoped<IFurnitureInStorageRepository, FurnitureInStorageRepository>();
            services.AddScoped<IFurnitureSaleRepository, FurnitureSaleRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<ICheckDetailsRepository, CheckDetailsRepository>();
            services.AddScoped<IEmployeeUserRepository, EmployeeUserRepository>();

            string connection = Configuration.GetConnectionString("FurnitureShopConnectionString");
            services.AddDbContext<FurnitureSaleContext>(options => options.UseSqlServer(connection));
            services.AddMvc();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
        //  app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authorization}/{action=Index}/{id?}");
            });
        }
    }
}
