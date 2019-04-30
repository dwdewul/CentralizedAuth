using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using CentralizedAuth.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CentralizedAuth.Api
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "WebApi";
                });
            services.AddDbContext<RuleAppDbContext>(options => { options.UseInMemoryDatabase("RuleAppDb"); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.Use((ctx, next) =>
            {
                var permissionsClaim = ctx.Request.Headers["x-permissions-token"];
                if (String.IsNullOrWhiteSpace(permissionsClaim))
                {
                    HttpClient client = new HttpClient();
                    var token = ctx.Request.Headers["Authorization"].ToString().Split(" ");
                    if (token.Length > 1)
                    {
                        client.SetBearerToken(token[1]);
                    }
                    var response = client.GetAsync("https://localhost:44314/api/permissions");
                    var permissions = response.Result.Headers.FirstOrDefault(x => x.Key.Equals("x-permissions-token"));
                    var permissionsValue = permissions.Value?.FirstOrDefault() ?? "";
                    if (String.IsNullOrWhiteSpace(permissionsValue))
                    {
                        Console.WriteLine("FUUUCK");
                        ctx.Response.Clear();
                        ctx.Response.StatusCode = 403;
                        ctx.Response.WriteAsync("Bad Request, m8").Wait();
                    }

                    Console.WriteLine("\n" + permissions.ToString() + "\n");
                    ctx.Request.Headers.Add(permissions.Key ?? "", permissions.Value?.First( ) ?? "");
                }

                return next();
            });
            app.UseMvc();
        }
    }
}
