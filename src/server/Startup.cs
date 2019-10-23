using AutoMapper;
using CaveCore.Profiles;
using CaveCore.Service;
using CaveCore.Service.Impl;
using CaveCore.Services;
using CaveCore.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace cave_server
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
            services.AddControllers();
            services.AddScoped<ITestService, CaveCoreTestService>();
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

            // add settings
            services.Configure<DbSettings>(
                          Configuration.GetSection(nameof(DbSettings)));
            services.AddSingleton<IDbSettings>(sp =>
                sp.GetRequiredService<IOptions<DbSettings>>().Value);

            // add services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICategoryService, CategoryService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
