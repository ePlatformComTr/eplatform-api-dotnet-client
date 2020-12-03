using ePlatform.Api.Core.Http;
using Microsoft.Extensions.Configuration;
using System;
using ePlatform.Api.eBelge.Ticket.Common;
using ePlatform.Api.eBelge.Ticket.EventTicket;
using ePlatform.Api.eBelge.Ticket.PassengerTicket;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TicketClientsExtensions
    {
        private static void AddTicketClients(this IServiceCollection services)
        {
            services.AddCoreClients();

            services.AddScoped<CommonTicketClient>();
            services.AddScoped<EventTicketClient>();
            services.AddScoped<PassengerTicketClient>();
        }

        private const string ePlatformClientOptionsSectionName = "ePlatformClientOptions";
        public static IServiceCollection AddePlatformTicketClients(this IServiceCollection services, IConfiguration configuration,
            string sectionName = ePlatformClientOptionsSectionName)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.ConfigureAndAddClientOptions(configuration, sectionName);
            services.AddTicketClients();

            return services;
        }

        public static IServiceCollection AddePlatformTicketClients(this IServiceCollection services, Action<ClientOptions> clientOptions)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            var options = new ClientOptions();
            var expr = clientOptions ?? delegate { };
            expr(options);

            services.ConfigureCoreClientOptions(options);
            services.Configure<ClientOptions>(opt => { opt.TicketServiceUrl = options.TicketServiceUrl; });
            services.AddSingleton(options);

            services.AddTicketClients();

            return services;
        }
    }
}
