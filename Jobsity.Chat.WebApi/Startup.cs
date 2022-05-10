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
using System.Threading.Tasks;
using Jobsity.Chat.WebApi.SignalR;
using Jobsity.Chat.Application.Handlers.Configuration;
using Jobsity.Chat.Application.Services.HostedServices;

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
                builder.SetIsOriginAllowed(origin => true);
            }));

            services.AddControllers();

            services.AddSignalR();

            services.AddHttpContextAccessor();

            services.AddDependiencies(Assembly.GetExecutingAssembly());
            var connectionString = Configuration["ConnectionString:Default"];
            services.AddDbContext<ChatContext>(options => options.UseSqlServer(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jobsity.Chat.WebApi", Version = "v1" });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                     ClockSkew = TimeSpan.Zero
                 });
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ChatContext>()
                .AddDefaultTokenProviders();

            services.ConfigureMediatR();
            services.AddHostedService<StockCodeHostedService>();
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

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureEndPoinsSignalR();
        }

    }
}
