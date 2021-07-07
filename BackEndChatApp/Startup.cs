using BackEndChatApp.Models;
using BackEndChatApp.Models.Hubs;
using BackEndChatApp.Respositories;
using BackEndChatApp.Respositories.Execution;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndChatApp
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

            ///tO AUTHENTICAte login
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "https://localhost:44380",
                        ValidAudience = "https://localhost:44380",
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes("superSecretKey@345"))
                    };
                });


            services.AddScoped<IUserPersonRes, UserPersonRes>();
            services.AddScoped<IMessagesRes, MessagesRes>();
            services.AddScoped<IGroupRes, GroupRes>();
            services.AddScoped<IUserLoginRes, UserLoginRes>();

            services.AddDbContext<ChatAppRealtimeContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ChatAppRealtime")));
           


            services.AddCors(o => o.AddPolicy("Corpolicy", builder =>
               {
                   builder.AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   .WithOrigins("http://localhost:4200");
               }));
            services.AddSignalR();

            services.AddControllers().AddNewtonsoftJson(opt=>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEndChatApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEndChatApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            ///to login
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("Corpolicy");
            app.UseEndpoints(end=>{
                end.MapHub<BroadcastHub>("/message");
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
