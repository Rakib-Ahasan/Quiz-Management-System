using Api.Config;
using Domain.Handlers.QuizHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RapidFireLib;

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
            services.AddRapidFire(new AppConfig());

            // Register your custom handlers (Domain services)
            services.AddScoped<IQuizHandler, QuizHandler>();
            services.AddScoped<IResultHandler, ResultHandler>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllOrigins",
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });
            //services.addauthentication(x =>
            //{
            //    x.defaultauthenticatescheme = jwtbearerdefaults.authenticationscheme;
            //    x.defaultchallengescheme = jwtbearerdefaults.authenticationscheme;
            //});
            services.AddMvc(options => options.EnableEndpointRouting = false);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc(routes =>
            {
                //routes.MapRoute("Default", "api/{controller}/{action}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}