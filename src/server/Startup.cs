using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CaveCore.Profiles;
using CaveCore.Service;
using CaveCore.Service.Impl;
using CaveCore.Services;
using CaveCore.Services.Impl;
using CaveServer.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
            services.AddCors();
            services.AddResponseCompression(options =>
            {
                ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "text/css",
                            "text/html",
                            "text/plain",
                            "text/xml",
                            "font/woff2",
                            "image/x-icon",
                            "image/svg+xml",
                            "image/png"});
            });
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
            // add user principle
            // avoid injecting IHttpContextAccessor in services other than server itself to avoid leaky abstractions
            services.AddTransient<ClaimsPrincipal>(s =>
                s.GetService<IHttpContextAccessor>().HttpContext.User);
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
            app.UseResponseCompression();
            app.UseCors(b => b.AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseDefaultFiles()
                .UseStaticFiles()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
