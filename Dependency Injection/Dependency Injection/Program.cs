using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dependency_Injection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    /***** IoC Inversion of Control (kontrolün ters çevrilmesi) *****
     * Bazý durumlarda sýnýfýmýz içinde çok fazla arayüz vsye referans vermemiz gerekebilir.
     * Bu gibi durumlarda tek tek dependency injection yazmak yerine
     * IoC yapýsý kurulur. IoC Containerinin içine gerekebilecek tüm türler konur ve gerektiði zaman içinden çaðrýlýr.
     * IoC yapýlanmasýný saðlayan çeþitli frameworkler vardýr (Structuremap, AutoFac, Ninject...)
     * Ayný zamanda .Net Core mimarisinin içinde de bir built-in konteyner mevcuttur
     * Built-in konteyner içine gelecek þeyleri 3 farklý davranýþla alabilir.
     * 
     * 1-Singleton
     * Uygulama çapýnda tek bir nesne oluþturur ve tüm taleplere o nesne döner.
     * 
     * 2-Scoped
     * Request baþýna bir nesne üretilir ve o requestin pipeline'ýnda olan tüm isteklere o nesne gönderilir.
     * 
     * 3-Transient
     * Her requestin her bir talebine yeni bir nesne üretir gönderir.
     * 
     *  --> KonteynerOrnek classýna git 
     */
}
