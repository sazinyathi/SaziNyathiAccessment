using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TritonExpress.Interfaces.Repositories;
using TritonExpress.Interfaces.Services;
using TritonExpress.Repositories;
using TritonExpress.Services;

namespace TritonExpress.API
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
            services.AddControllers();
            services.AddDbContext<TritonExpressDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample TritonExpress API" });
            });
            #region Business Logic Injections
            services.AddTransient<IProvincesServices, ProvincesServices>();
            services.AddTransient<IBranchesService, BranchesService>();
            services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IVehicleTypeService, VehicleTypeService>();
            services.AddTransient<IWayBillsRepository, WayBillsRepository>();
            #endregion

            #region DAL Injections
            services.AddTransient<IProvincesRepository, ProvincesRepository>();
            services.AddTransient<IBranchesRepository, BranchesRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IVehicleTypeService, VehicleTypeService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IWayBillsService, WayBillsService>();


            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint(url:"/swagger/v1/swagger.json",name: " Sample TritonExpress API");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
