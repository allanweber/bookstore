using AutoMapper;
using Bookstore.Framework.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Bookstore.Framework.Constants;
using Bookstore.Framework.Filters;
using Bookstore.Framework.Middlewares;
using Bookstore.Domain.Repositories;
using Bookstore.Infrastructure.Repositories;

namespace Bookstore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrincipalDbContext>(options =>
            {
                if (this.Environment.IsEnvironment("IntegrationTests"))
                {
                    options.UseInMemoryDatabase("IntegrationTests");
                }
                else
                {
                    options.UseSqlServer(this.Configuration.GetConnectionString("RelationalConnection"));
                }
            });

            services.AddMvc().AddMvcOptions(setup => setup.Filters.Add<CommandResultFilterAttribute>());

            services.AddAutoMapper();

            services.AddMediatR();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                info: new Info
                {
                    Title = "Bookstore",
                    Version = "v1",
                    Description = "Bookstore App",
                    Contact = new Contact
                    {
                        Name = "Allan Cassiano Weber",
                        Url = "https:/github.com/allanweber"
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Bookstore.xml");
                s.IncludeXmlComments(xmlPath);
            });

            services.AddCors(o => o.AddPolicy(AppConstants.ALLOWALLHEADERS, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBookRepository, BookRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvcWithDefaultRoute();

            app.UseCors(AppConstants.ALLOWALLHEADERS);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Bookstore");
            });
        }
    }
}
