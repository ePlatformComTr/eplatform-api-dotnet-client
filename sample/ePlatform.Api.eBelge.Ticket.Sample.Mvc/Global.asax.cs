using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Mvc;
using ePlatform.Api.Core.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ePlatform.Api.eBelge.Ticket.Sample.Mvc
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var services = new ServiceCollection();
            services.AddDistributedMemoryCache(); //Token cache için gerekli
            services.AddePlatformTicketClients(clientOptions =>
            {
                clientOptions.AuthServiceUrl = "https://coretest.isim360.com";
                clientOptions.TicketServiceUrl = "https://ebiletservicetest.isim360.com";
                clientOptions.Auth = new ClientOptions.AuthOption
                {
                    ClientId = "serviceApi",
                    Username = "serviceuser01@isim360.com",
                    Password = "ePlatform123+"
                };
            });

            var providerFactory = new AutofacServiceProviderFactory();
            //to populate your container
            ContainerBuilder builder = providerFactory.CreateBuilder(services);

            //var builder = new ContainerBuilder();

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(WebApiApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // ePlatform Services
            //builder.RegisterModule<ePlatformSerivecesModule>();

            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // Standard MVC setup:

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
