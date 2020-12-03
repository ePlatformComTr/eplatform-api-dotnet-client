using ePlatform.Api.Core.Http;
using Microsoft.Extensions.Configuration;
using System;
using ePlatform.Api.eBelge.Invoice;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InvoiceClientsExtensions
    {
        private static void AddInvoiceClients(this IServiceCollection services)
        {
            services.AddCoreClients();

            services.AddScoped<OutboxInvoiceClient>();
            services.AddScoped<InboxInvoiceClient>();
            services.AddScoped<CommonClient>();
            services.AddScoped<EArchiveInvoiceClient>();
        }

        private const string ePlatformClientOptionsSectionName = "ePlatformClientOptions";
        public static IServiceCollection AddePlatformInvoiceClients(this IServiceCollection services, IConfiguration configuration,
            string sectionName = ePlatformClientOptionsSectionName)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.ConfigureAndAddClientOptions(configuration, sectionName);
            services.AddInvoiceClients();

            return services;
        }
        
        [Obsolete("This usage is obsolete now in the next versions this method will be removed. Please use AddePlatformInvoiceClients instead.")]
        public static IServiceCollection AddePlatformClients(this IServiceCollection services, IConfiguration configuration,
            string sectionName = ePlatformClientOptionsSectionName)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));


            services.ConfigureAndAddClientOptions(configuration, sectionName);
            services.AddInvoiceClients();

            return services;
        }

        public static IServiceCollection AddePlatformInvoiceClients(this IServiceCollection services, Action<ClientOptions> clientOptions)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            var options = new ClientOptions();
            var expr = clientOptions ?? delegate { };
            expr(options);

            services.ConfigureCoreClientOptions(options);
            services.Configure<ClientOptions>(opt => { opt.InvoiceServiceUrl = options.InvoiceServiceUrl; });
            services.AddSingleton(options);

            services.AddInvoiceClients();

            return services;
        }

        [Obsolete("This usage is obsolete now in the next versions this method will be removed. Please use AddePlatformInvoiceClients instead.")]
        public static IServiceCollection AddePlatformClients(this IServiceCollection services, Action<ClientOptions> clientOptions)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            var options = new ClientOptions();
            var expr = clientOptions ?? delegate { };
            expr(options);

            services.ConfigureCoreClientOptions(options);
            services.Configure<ClientOptions>(opt => { opt.InvoiceServiceUrl = options.InvoiceServiceUrl; });
            services.AddSingleton(options);

            services.AddInvoiceClients();

            return services;
        }

    }
}
