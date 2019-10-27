using System.Text;
using AutoMapper;
using CaveCore.Profiles;
using CaveCore.Service;
using CaveCore.Service.Impl;
using CaveCore.Services;
using CaveCore.Settings;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            // add settings
            services.Configure<DbSettings>(
                Configuration.GetSection(nameof(DbSettings)));
            services.Configure<AppSettings>(
            Configuration.GetSection(nameof(AppSettings)));

            // add conventions
            SetupMongoConvention();
            // add db instance
            services.AddSingleton<IMongoClient>(
               s => new MongoClient(Configuration.GetConnectionString("default")));
            // add services
            services.AddScoped<ITestService, CaveCoreTestService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICategoryService, CategoryService>();
        }
        private void SetupMongoConvention()
        {
            // Set up MongoDB conventions
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
