using System;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using ePlatform.Api.eBelge.Ticket.Tests.Builders;
using ePlatform.Api.eBelge.Ticket.Tests.Builders.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Ticket.Tests.Setup
{
    public class StartupFixture
    {
        public IServiceProvider ServiceProvider { get; }

        public StartupFixture()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var services = new ServiceCollection();

            services.AddDistributedMemoryCache().AddePlatformTicketClients(configuration);

            services.AddScoped<TicketBuilderModelBuilder>();
            services.AddScoped<IBuilder<NoteModel, NoteModelBuilder>, NoteModelBuilder>();
            services.AddScoped<IBuilder<TaxModel, TaxModelBuilder>, TaxModelBuilder>();
            services.AddScoped<IBuilder<TicketLine, TicketLineBuilder>, TicketLineBuilder>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }

    [CollectionDefinition("ticket-startup")]
    public class StartupCollection : ICollectionFixture<StartupFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
