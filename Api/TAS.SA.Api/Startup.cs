using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TAS.SA.Api.BackgroundServices;
using TAS.SA.Dominio;
using TAS.SA.Infra;
using TAS.SA.Infra.Config;

namespace TAS.SA.Api
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
            services.AddDbContext<ServicoAnuncioContexto>(opcao => opcao.UseInMemoryDatabase("ServicoDeAnuncios"));

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper();

            services.AddScoped<IAnuncioRepositorio, AnuncioRepositorio>();
            // services.AddHostedService<ManipuladorFecharAnuncio>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Serviço cadastro de lances", Version = "v1" });
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseCors();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço cadastro de lances");
            });

            using (var serviceScope = app.ApplicationServices.CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<ServicoAnuncioContexto>();

                DbInicializacao.PopularDados(context);
            }
        }
    }
}
