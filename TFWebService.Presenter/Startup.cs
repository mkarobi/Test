using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;
using TFWebService.Common.Helper;
using TFWebService.Common.Helpers;
using TFWebService.Data.DatabaseContext;
using TFWebService.Presenter.Helper.Filter;
using TFWebService.Repo.Infrastructure;
using TFWebService.Services.Common.Encrypt.Interface;
using TFWebService.Services.Common.Encrypt.Service;
using TFWebService.Services.Site.Admin.Auth.Interface;
using TFWebService.Services.Site.Admin.Auth.Service;
using TFWebService.Services.Upload.Interface;
using TFWebService.Services.Upload.Service;

namespace TFWebService.Presenter
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
            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            //services.AddMvc(opt => opt.EnableEndpointRouting = false)
            //    .AddNewtonsoftJson(opt =>
            //    {
            //        opt.SerializerSettings.ReferenceLoopHandling =
            //        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //    });

            services.AddCors();


            //Db
            //services.AddDbContext<TFDbContext>(options =>
            //    options.UseSqlite($"Data Source ={Path.Combine(Environment.CurrentDirectory, "TFDb.db")}")
            //        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            var con = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            services.AddDbContext<TFDbContext>(options =>
                options.UseNpgsql(con)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));




            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            //Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEncryptService, EncryptService>();
            services.AddScoped<UserCheckAdminFilter>();
            services.AddScoped<UserCheckTokenFilter>();


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            //Use JWT Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //NSwag
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "Api";
                document.ApiGroupNames = new[] { "WebService" };
                document.PostProcess = d =>
                {
                    d.Info.Title = "Tanasob Fitness Webservice";
                    d.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Vikky Patterson",
                        Email = "vikkyPatterson8173@gmail.com",
                    };
                };
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT Token}."
                });
                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "CANDC";
                document.ApiGroupNames = new[] { "CANDC" };
                document.PostProcess = d =>
                {
                    d.Info.Title = "Tanasob Fitness CANDC";
                    d.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Vikky Patterson",
                        Email = "vikkyPatterson8173@gmail.com",
                    };
                };
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT Token}."
                });
                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
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
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddAppError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //NSwag
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(opt =>
            {
                opt.Path = "/redoc";
                opt.DocumentPath = "/swagger/v1/swagger.json";
            });

            //StaticFiles
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = new PathString("/wwwroot")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
