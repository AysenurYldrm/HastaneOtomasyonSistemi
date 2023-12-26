
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Models; // Kullanıcı modelinizi içeren namespace
using HastaneOtomasyonSistemi.Data;
using Microsoft.AspNetCore.Session;

namespace HastaneOtomasyonSistemi
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
            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddDbContext<HastaneOtomasyonSistemiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("Admin", options =>
                {
                    options.LoginPath = "/Admin/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa

                })
                .AddCookie("HomeLogin", options =>
                 {
                     options.LoginPath = "/HomeLogin/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa
                 });
				
			services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSession();
            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication(); // Yetkilendirme kullanımı ekleyin
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}

