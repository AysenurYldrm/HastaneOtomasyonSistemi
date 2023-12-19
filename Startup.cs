
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonSistemi.Models; // Kullanıcı modelinizi içeren namespace
using HastaneOtomasyonSistemi.Data;

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
            services.AddDbContext<HastaneOtomasyonSistemiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("Admin",options =>
                {
                    options.LoginPath = "/Admin/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa
               
                })
                .AddCookie("User", options =>
			     {
				    options.LoginPath = "/Hasta/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa
			    })
				.AddCookie("Userdok", options =>
				{
					options.LoginPath = "/Doktor/Login"; // Giriş yapılmamışsa yönlendirilecek sayfa
				}); ;

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

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

