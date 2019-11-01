using CaveCore.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace CaveServer.Extentions
{
    public static class ServerExtentions
    {
        public static IServiceCollection AddMongoDbInstance(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            // use local appsetting files
            if (env.IsDevelopment())
            {
                services.AddSingleton<IMongoClient>(
                           s => new MongoClient(config.GetConnectionString("default")));
            }
            // use env vars
            if (env.IsProduction())
            {
                 services.AddSingleton<IMongoClient>(
                           s => new MongoClient(config.GetEnvDbConnection()));
            }
            // add conventions
            // Set up MongoDB conventions
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
            return services;

        }
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            // use local appsetting files
            if (env.IsDevelopment())
            {
                services.Configure<DbSettings>(
                    config.GetSection(nameof(DbSettings)));
                services.Configure<AppSettings>(
                    config.GetSection(nameof(AppSettings)));
            }
            // use env vars
            if (env.IsProduction())
            {
                services.Configure<DbSettings>(s => {
                    // TODO: check variable availablity
                    s.DatabaseName = config.GetValue(typeof(string), "DATABASE_NAME").ToString();
                    s.UserCollectionName = config.GetValue(typeof(string), "USER_COLLECTION_NAME").ToString();
                    s.CategoryCollectionName = config.GetValue(typeof(string), "CATEGORY_COLLECTION_NAME").ToString();
                    s.PostCollectionName = config.GetValue(typeof(string), "POST_COLLECTION_NAME").ToString();
                });
                services.Configure<AppSettings>(s => {
                    s.JwtSecret = config.GetValue(typeof(string), "JWTSECRET").ToString();
                });
            }
            return services;
        }

        private static string GetEnvDbConnection(this IConfiguration config)
        {
            return config.GetValue(typeof(string), "CONNSTR").ToString();
        }
    }
}