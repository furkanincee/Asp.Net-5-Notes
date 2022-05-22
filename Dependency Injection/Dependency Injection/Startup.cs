using Dependency_Injection.Services;
using Dependency_Injection.Services.Interfaces;
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

namespace Dependency_Injection
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
            services.Add(new ServiceDescriptor(typeof(ConsoleLog), new ConsoleLog())); // add ile bir þey eklediðimizde default olarak singleton olarak ekler.
            services.AddSingleton<ConsoleLog>(); // ConsoleLog türünden bir nesne alýp istek geldiðinde onu gönderecek
            services.AddSingleton<TextLog>(p => new TextLog(5)); // constructorunda parametre alan þeyler için ise bu þekilde parametresini veriyoruz. Yani bu nesneden 5 parametresiyle örnek oluþtur dedik.
            services.AddScoped<ConsoleLog>(); // yine ayný bu þekilde scoped veya transient olarak da ekleyebiliyoruz

            services.AddScoped<ILog>(p => new TextLog(5)); // Bir yerde ILog isteði yapýldýðýnda buradaki verdiðimiz TextLog dönecek. Eðer ileride ihtiyaç deðiþir ve ConsoleLog kullanmak istersek de 
                                                           // uygulamada yapacaðýmýz tek iþ buradaki TextLogu ConsoleLog ile deðiþtirmek olacak
            services.AddScoped<ILog, ConsoleLog>(); // Bu þekilde de kullaným var. Kritik nokta genericteki 1.parametre interface 2. parametre ise o interfaceden kalýtýlmýþ bir sýnýf olmalý.
                                                    // ayrýca ctoru parametre almýyor olmalý ki böyle kullanabilelim.

            // --> HomeControllera geç

            services.AddControllersWithViews();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
