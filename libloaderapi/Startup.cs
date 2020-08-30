using libloaderapi.Domain.Database;
using libloaderapi.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Text;
using System.Text.Json.Serialization;
using libloaderapi.Domain.Extensions.Configuration;
using libloaderapi.Domain.Filters;
using Microsoft.AspNetCore.HttpOverrides;

namespace libloaderapi
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
            // Add swagger config
            services.AddCustomSwaggerConfig();

            // Forward headers
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Secret"])),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            // DB
            var builder = new NpgsqlConnectionStringBuilder
            {
                ConnectionString = Configuration.GetConnectionString("Postgres"),
                Username = Configuration["UserID"],
                Password = Configuration["Password"]
            };
            services.AddDbContext<AppDbContext>(optionsBuilder => 
                optionsBuilder.UseNpgsql(builder.ConnectionString));

            services.AddCors();

            services
                .AddRouting(opts => opts.LowercaseUrls = true)
                .AddControllers(opts => opts.Filters.Add<DevelClientActionFilter>())
                .AddJsonOptions(opts => 
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            
            // Custom services
            services
                .AddSingleton<IAnalyserService, AnalyserService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IUsersService, UsersService>()
                .AddSingleton<IBlobService, BlobService>()
                .AddScoped<IClientsService, ClientsService>();

            // Telemetry
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context)
        {
            context.Database.Migrate();

            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();


            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "libloader API V2");
            });

            app.UseRouting();
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}