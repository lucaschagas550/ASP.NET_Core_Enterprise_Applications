using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using System.Globalization;

namespace NSE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<AppSettings>(configuration);
        }

        public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/error/500"); //Caminho para os erros não tratados
            //    app.UseStatusCodePagesWithRedirects("/erro/{0}"); // Caminho para erros tratados
            //    app.UseHsts();
            //}

            app.UseExceptionHandler("/error/500"); //Caminho para os erros não tratados
            app.UseStatusCodePagesWithRedirects("/erro/{0}"); // Caminho para erros tratados
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration(); // deve ficar entre useRouting e UseEndPoint a autenticação

            var supportedCultures = new[] { new CultureInfo("pt-BR") }; //todas culturas suportadas estão aqui
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"), //cultura padrão eh pt br para a internacionalização esse seria o caminho, conforme a cultura escolhida enviar para uma rota ou um cookie
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalogo}/{action=Index}/{id?}");
            });
        }
    }
}
