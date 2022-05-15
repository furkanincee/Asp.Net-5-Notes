using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
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
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Tüm middleware'ler burada, configure metodunun içinde çaðrýlýr. .Net mimarisinde tüm middleware'ler use adýyla baþlar.
            // Middlewareler iç içe sarmal yapýda bulunur yani biri çalýþýp sonlanmadan bir diðeri çaðrýlýr. Dolayýsýyla tetiklenme sýrasý önemlidir.
            // Örneðin UseRouting UseEndpointsten önce gelmelidir. Önce rota eþleþmeli ki hangi controllerin tetikleneceðini bilelim.
            // Authentication de authorizationdan önce tetiklenmelidir. Önce kimlik doðrulanýr sonra yetkilendirme yapýlýr.

            // 4 tane temel hazýr middleware vardýr
            /* Run --> Tetiklendiði zaman kendinden sonra gelen middleware'leri tetiklemez akýþ sonlanýr. Bu özellike short circuit (kýsa devre) denir.
             * Use
             * Map
             * MapWhen
             * 
             * *** Use metodu ***
             * use kullandýðýmýzda sýradaki middleware'i çaðýrýp ve çaðrýlan middleware iþlemi bitirdikten sonra
             * geriye dönüp devam eden bir yapý oluþur.
             * Task bir sonraki çalýþtýrýlacak middleware'i temsil eder. task.Invoke() ile o metot çalýþtýrýlýr.
             * app.Use(async (context, task) =>
             * {
             *      Console.WriteLine("Start use middleware");
             *      await task.Invoke();
             *      Console.WriteLine("Stop use middleware")
             * });
             * app.Run(async c =>
             * {
             *      Console.WriteLine("Start Run middleware");
             * });
             * Bu kod bloðunun konsolda çýktýsý;
             * Start use middleware
             * Start run middleware
             * Stop use middleware 
             * olacaktýr.
             * 
             * *** Map metodu ***
             * Gelen talebin pathine göre farklý middlewareler çalýþtýrmamýzý saðlar
             * 
             * app.Map("/weatherforecast", builder =>
             * {
             *      builder.Run(async c => c.Response.WriteAsync("Run tetiklendi"));
             * });
             * 
             * *** MapWhen metodu ***
             * Mapte sadece requestin yapýldýðý pathe göre filtreleme yapýlýrken, MapWhen ile requestin herhangi bir özelliðine göre filtreleme yapýlabilir.
             * 
             * app.MapWhen(c => c.Request.Method == "GET", builder =>
             * {
             *      builder.Use(async (context, task) =>
             *      {
             *          Console.WriteLine("start use middleware");
             *          await task.Invoke();
             *          Console WriteLine("stop use middleware");
             *      });
             * });
             * 
             * kendi middlewareimizi oluþturduk --> Middlewares klasörünün içinde
             */

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
