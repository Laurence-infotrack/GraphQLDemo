using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using GraphQLDemo.Data;
using GraphQLDemo.Model;
using GraphQL.Types;

namespace GraphQL
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LdmcoreContext>(opt => opt.UseInMemoryDatabase());

            // Add framework services.
            services.AddScoped<LdmcoreQuery>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<OrderType>();
            services.AddTransient<ServiceType>();
            services.AddTransient<LoginType>();
            var sp = services.BuildServiceProvider();
            services.AddScoped<Types.ISchema>(_ => new LdmcoreSchema(type => (GraphType)sp.GetService(type)) { Query = sp.GetService<LdmcoreQuery>() });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var context = app.ApplicationServices.GetService<LdmcoreContext>();
            TestData.AddTestData(context);
            app.UseMvc();
        }
    }
}
