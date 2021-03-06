using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Nest.BaseCore.Gateway
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
            services.AddControllers();

            //添加Ocelot，注意 ocelot.json的路径，此处为根路径下
            services.AddOcelot(new ConfigurationBuilder().AddJsonFile("ocelot.json", true, true).Build());

            //services.AddIdentityServer()
            //    .
            //    .AddExtensionGrantValidator<SignatureGrantValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseOcelot().Wait();

            app.UseOcelot((builder, config) =>
            {

                //builder.BuildCustomOcelotPipeline(config)

                //.UseMiddleware<ThemeCssMinUrlReplacer>()

                //.Build();

               // builder.BuildOcelotPipeline(config).UseMiddleware<SignatureValidatorMiddleware>().Build();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
