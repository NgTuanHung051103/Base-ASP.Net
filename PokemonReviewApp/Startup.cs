using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;
using PokemonReviewApp.Services;
using System.Text.Json.Serialization;

namespace PokemonReviewApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment) 
        {
            Configuration = configuration;
            Environment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddCors();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")!);
            });

            services.AddControllers();
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                );
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IOwnerServices, OwnerServices>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewerRepository, ReviewerRepository>();
            services.AddEndpointsApiExplorer();
            AddSwaggerUI(services);
        }

        public void Configure(WebApplication app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "POKEMON APIs V1");
            });

            app.UseForwardedHeaders();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapFallbackToFile("/index.html");

            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
            );
        }

        private static void AddSwaggerUI(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "POKEMON APIs",
                    Version = "v1.0",
                    Description = "NabatiiCheese Group",
                });
            });
        }
    }
}