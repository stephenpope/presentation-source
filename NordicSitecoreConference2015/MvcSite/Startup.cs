using System;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Microsoft.Framework.DependencyInjection;
using Autofac.Framework.DependencyInjection;
using Owin;
using Microsoft.Framework.Configuration;
using System.IO;
using Microsoft.Framework.Configuration.Memory;

//[assembly: OwinStartup(typeof(SitecoreDnxDi.Startup))]
namespace MvcSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var serviceProvider = ConfigureServices(new ServiceCollection());

            ConfigureForMvc(serviceProvider);

            ConfigureForWebForms(serviceProvider);
        }

        private IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ExampleOptions>(GetConfiguration());

            services.AddTransient<IExampleService, ExampleService>(provider => new ExampleService { SomeData = "Hello from Microsoft.Framework.DependencyInjection" });

            return services.BuildServiceProvider();
        }

        #region Part One

        private void ConfigureForMvc(IServiceProvider serviceProvider)
        {
            DependencyResolver.SetResolver(new BasicDependencyResolver(serviceProvider));
        }

        private void ConfigureForWebForms(IServiceProvider serviceProvider)
        {
            HttpContext.Current.Application["serviceProvider"] = serviceProvider;
        }

        #endregion

        #region Part Two

        private IServiceProvider ConfigureServicesAutoFac(IServiceCollection services)
        {
            services.AddTransient<IExampleService, ExampleService>(provider => new ExampleService { SomeData = "Hello from AutoFac!" });

            var builder = new ContainerBuilder();

            // Add any Autofac registrations.
            builder.Register(c => new LogThings())
                    .As<ILogThings>()
                    .InstancePerLifetimeScope();

            // Populate the services.
            builder.Populate(services);

            // Build the container.
            var container = builder.Build();

            // Resolve and return the service provider.
            return container.Resolve<IServiceProvider>();
        }

        #endregion

        #region Part Three

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            builder.AddJsonFile(path);

            return builder.Build();
        }

        #endregion
    }
}