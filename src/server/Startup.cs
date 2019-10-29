using System.Text;
using AutoMapper;
using CaveCore.Profiles;
using CaveCore.Service;
using CaveCore.Service.Impl;
using CaveCore.Services;
using CaveCore.Settings;
using CaveServer.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CaveServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddCors();
            services.AddControllers();
            // add auth
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:JwtSecret").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            // routing conventions
            services.AddRouting(o =>
                {
                    o.LowercaseUrls = true;
                    o.LowercaseQueryStrings = true;
                });
            // mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DefaultProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            // add context
            services.AddHttpContextAccessor();
            // add settings & db
            services.AddMongoDbInstance(CurrentEnvironment, Configuration)
                .AddAppSettings(CurrentEnvironment, Configuration);
            // add services
            services.AddScoped<ITestService, CaveCoreTestService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IPostService, PostService>();
        }
       
      

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(b => b.AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseAuthentication()
                .UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
