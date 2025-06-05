using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.IO.Compression;
using System.Text.Json.Serialization;
using BookStoreAPI.Controllers;
using BookStoreAPI.Domain.Global;
using BookStoreAPI.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Infrastructure
{
    public static class DependencyInjectionInfra
    {
        public static IServiceCollection AddApplicationInfrastructure(this IServiceCollection services, IWebHostEnvironment hostEnvironment)
        {
            ConfigurationMaster settings;

            IConfiguration configuration;
            //string configurationPath = "..\\configuration.json";
            //if (!File.Exists(configurationPath))
            //    throw new FileNotFoundException("Configuration file was not found.");
            configuration = new ConfigurationBuilder()
                                           .SetBasePath(hostEnvironment.ContentRootPath)
                                           .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                                           .Build();


            services.Configure<ConfigurationMaster>(configuration);
            settings = services.GetConfigurations();
            services.AddSingleton(settings.Logging);
            services.AddSingleton(settings);

            services.AddDbContext<ApplicationReadContext>(options => options.UseSqlServer(settings.ConnectionStrings.Connection));
            services.AddDbContext<ApplicationWriteContext>(options => options.UseSqlServer(settings.ConnectionStrings.Connection));


            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", builder =>
                {
                    builder.WithOrigins(settings.JWTKeyes.AllowedOrigins.ToArray())
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
                });
            });

            #region Service Response Compression

            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            #endregion

            #region API Common
            services.AddMemoryCache();

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"BookStoreAPI Services End Point",
                    Description = "Authentication and Authorization Updates"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "description",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}



                   }
                });
                swagger.AddSecurityDefinition("oauthkey", new OpenApiSecurityScheme()
                {
                    Description = "Api key needed to access the endpoints",
                    In = ParameterLocation.Header,
                    Name = "oauthkey",
                    Type = SecuritySchemeType.ApiKey
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "oauthkey",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauthkey"
                            },
                         },
                     new string[] {}
                    }
                });
            });
            #endregion

            services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
            
            return services;
        }

        private static ConfigurationMaster GetConfigurations(this IServiceCollection services)
        {
            ServiceProvider sp = services.BuildServiceProvider();
            IOptions<ConfigurationMaster> iop = sp?.GetService<IOptions<ConfigurationMaster>>();
            return iop?.Value;
        }

        public static WebApplication UseApplicationInfrastructure(this WebApplication app)
        {
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "Rest Services v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseResponseCompression();

            app.MapControllers();

            var scope = app.Services.CreateScope();
            if (scope != null)
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "//AppData"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "//AppData");
                }
                var db = scope.ServiceProvider.GetService<ApplicationAuditLogContext>();
                db?.Database.EnsureCreated();
            }
            return app;
        }
    }
}
