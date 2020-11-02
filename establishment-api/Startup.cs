using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using establishment_repository.Establishment;
using establishment_repository.Interface;
using establishment_service.Establishment;
using establishment_service.Interface;
using establishment_service.Validate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace establishment_api
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

            services.AddTransient<IEstablishmentRepository, EstablishmentRepository>();
            services.AddTransient<IEstablishmentAddressRepository, EstablishmentAddressRepository>();
            services.AddTransient<IEstablishmentAccountRepository, EstablishmentAccountRepository>();
            services.AddTransient<IEstablishmentCategoryRepository, EstablishmentCategoryRepository>();
            services.AddTransient<IEstablishmentService, EstablishmentService>();
            services.AddTransient<IEstablishmentCategoryService, EstablishmentCategoryService>();
            services.AddTransient<IValidateService, ValidateService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Estabelecimento API",
                        Version = "v1",
                        Description = "Exemplo de API REST criada com o ASP.NET Core",
                        Contact = new OpenApiContact
                        {
                            Name = "Daniel Richard",
                            Url = new Uri("https://github.com/DanielRichard-Dev")
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.WithOrigins("http://127.0.0.1:5500/").AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json",
                    "Estabelecimento API");
            });
        }
    }
}
