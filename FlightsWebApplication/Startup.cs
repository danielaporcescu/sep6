using DataContext.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.Repositories;
using Services.Repositories.Interfaces;

namespace FlightsWebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UAAContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AzureDbConnection")));

            services.AddMvc();

            services.AddTransient<IAirlineRepository, AirlineRepository>();
            services.AddTransient<IFlightsRepository, FlightsRepository>();
            services.AddTransient<IWeatherRepository, WeatherRepository>();
            services.AddTransient<IPlanesRepository, PlanesRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORS rules",
                                  builder =>
                                  {
                                      //builder.WithOrigins("http://localhost:3000/",
                                      //                    "https://uaa-web-app.azurewebsites.net",
                                      //                    "http://192.168.50.76:3000/",
                                      //                    "http://192.168.50.76");
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UAA Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<UAAContext>();
                context.Database.Migrate();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UAA Api V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("CORS rules");
            app.UseEndpoints(endpoints =>

            {
                endpoints.MapControllers();
            });
        }
    }
}