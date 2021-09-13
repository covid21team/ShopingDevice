using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API
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
            var connection = Configuration.GetConnectionString("CovidDatabase");

            services.AddDbContextPool<COVIDContext>(options => options.UseSqlServer(connection));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //JsonObject (Quiz)

            //services.AddMvc().AddNewtonsoftJson();

            //thêm vào token JWT

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>

            {

                option.RequireHttpsMetadata = false;

                option.SaveToken = true;

                option.TokenValidationParameters = new TokenValidationParameters()

                {

                    ValidateIssuer = true,

                    ValidateAudience = true,

                    ValidAudience = Configuration["Jwt:Audience"],

                    ValidIssuer = Configuration["Jwt:Issuer"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),

                    ClockSkew = TimeSpan.Zero //mặc định token sẽ có thêm 5ph so với expires, thêm ClockSkew để bỏ đi.

                };

            });

            /*services.AddDbContext<COVIDContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlDbContext"));
                options.UseLazyLoadingProxies(false);
            });*/
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
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
