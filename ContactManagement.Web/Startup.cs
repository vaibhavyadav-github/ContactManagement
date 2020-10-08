

namespace ContactManagement.Web
{
    using AutoMapper;

    using ContactManagement.Data;
    using ContactManagement.Data.Abstraction;
    using ContactManagement.Web.MapperProfiles;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using System.Reflection;
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

            services.AddMvc(config =>
            {
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
          .AddJsonOptions(options =>
          {
              options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
              options.SerializerSettings.Formatting = Formatting.Indented;
              options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          });


            services.AddAutoMapper(typeof(ContactProfile).GetTypeInfo().Assembly);
            //services.AddTransient<IMapper, Mapper>();
            services.AddDbContext<ContactDbContext>(option =>
                            option.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddSwaggerGen((option) =>
                {
                    option.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactApi", Version = "v1" });
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ContactDbContext dBContext)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
                               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contract Api")
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            dBContext.Database.Migrate();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(new { error = exception.Message });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
