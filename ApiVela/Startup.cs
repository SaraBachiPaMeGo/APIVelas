using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiVela.Data;
using ApiVela.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ApiVela
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

            String candena = Configuration.GetConnectionString("proyektVelas");
            services.AddDbContext<Contexto>(options => options.UseSqlServer(candena));
            
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(name: "v1",
                                    new OpenApiInfo
                                    {
                                        Title = "Api velas",
                                        Version = "v1",
                                        Description = ""
                                    });
            });

            services.AddTransient<RepositoryCeras>();
            services.AddTransient<RepositoryClientes>();
            services.AddTransient<RepositoryEndurecedores>();
            services.AddTransient<RepositoryFragancias>();
            services.AddTransient<RepositoryMechas>();
            services.AddTransient<RepositoryMoldes>();
            services.AddTransient<RepositoryPacks>();
            services.AddTransient<RepositoryPedidos>();
            services.AddTransient<RepositoryPigmentos>();
            services.AddTransient<RepositoryVelas>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            //UI indica dónde va a visualizar el usuario la documentación
            //generada por SWAGGER en nuestro servidor
            app.UseSwaggerUI(
                c => {
                    //varias rutas
                    //Debemos configurar la url del servidor para la documentación
                    c.SwaggerEndpoint(
                        url: "swagger/v1/swagger.json",
                        name: "Api velas v1"
                        );
                    c.RoutePrefix = "";
                });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
