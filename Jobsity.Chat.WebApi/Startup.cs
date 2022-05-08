using Jobsity.Chat.Data.Context;
using Jobsity.Chat.Domain.Models;
using Jobsity.Chat.IoC.NativeInjector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace Jobsity.Chat.WebApi
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
            services.AddCors(options => options.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            services.AddControllers();

            var key = Configuration["OAuth:Secret"];
            ConfigureAuth(services, key);


            services.AddDependiencies(Assembly.GetExecutingAssembly());
            var connectionString = Configuration["ConnectionString:Default"];
            services.AddDbContext<ChatContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ChatContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jobsity.Chat.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jobsity.Chat.WebApi v1"));
            }
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureAuth(IServiceCollection services, string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            var defaultScheme = JwtBearerDefaults.AuthenticationScheme;
            var jwtPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(defaultScheme)
                        .RequireAuthenticatedUser()
                        .Build();

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(defaultScheme, jwtPolicy);
            })
            .AddAuthentication(options =>
            {
                options.DefaultScheme = defaultScheme;
                options.DefaultAuthenticateScheme = defaultScheme;
                options.DefaultForbidScheme = defaultScheme;
                options.DefaultSignInScheme = defaultScheme;
                options.DefaultSignOutScheme = defaultScheme;
                options.DefaultChallengeScheme = defaultScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
