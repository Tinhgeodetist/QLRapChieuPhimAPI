using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QLRapChieuPhimAPI.Auth;
using Service.Base;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLRapChieuPhimAPI
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            //services.Configure<Jwt>(Configuration.GetSection("Jwt:SecretKey"));
            Jwt.SecretKey = Configuration.GetSection("Jwt:SecretKey").Value;
            services.AddDbContext<QLRapChieuPhimContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IXuLyPhim, XuLyPhim>();
            services.AddScoped<IXuLyTheLoaiPhim, XuLyTheLoaiPhim>();
            services.AddScoped<IXuLyXepHangPhim, XuLyXepHangPhim>();
            services.AddScoped<IXuLySlideBanner, XuLySlideBanner>();
            services.AddScoped<IXuLyRap, XuLyRap>();
            services.AddScoped<IXuLyPhong, XuLyPhong>();
            services.AddScoped<IXuLyThanhVien, XuLyThanhVien>();
            services.AddScoped<IXuLyNhanVien, XuLyNhanVien>();
            services.AddScoped<IXuLySuatChieu, XuLySuatChieu>();
            services.AddScoped<IXuLyGhe, XuLyGhe>();
            services.AddScoped<IXuLyLoaiGhe, XuLyLoaiGhe>();
            services.AddScoped<IXuLyThongTinGhe, XuLyThongTinGhe>();
            services.AddScoped<IXuLyLichChieu, XuLyLichChieu>();
            services.AddScoped<IXuLyVe, XuLyVe>();
            services.AddControllers();
            services.AddMemoryCache();
            services.AddSingleton<Jwt>();
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddHostedService<JwtRefreshTokenCache>();
            byte[] key = Encoding.ASCII.GetBytes(Configuration.GetSection("Jwt:SecretKey").Value);
            services.AddAuthentication(p =>
            {
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(p =>
            {
                p.RequireHttpsMetadata = false;
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
