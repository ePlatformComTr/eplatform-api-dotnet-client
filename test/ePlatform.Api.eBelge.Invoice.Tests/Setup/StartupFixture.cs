using System;
using ePlatform.Api.eBelge.Invoice.Models;
using ePlatform.Api.eBelge.Invoice.Tests.Builders;
using ePlatform.Api.eBelge.Invoice.Tests.Builders.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Invoice.Tests
{
    public class StartupFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public StartupFixture()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var services = new ServiceCollection();

            services.AddDistributedMemoryCache().AddePlatformInvoiceClients(configuration);

            services.AddScoped<UblBuilderModelBuilder>();
            services.AddScoped<IBuilder<GeneralInfoBaseModel, GeneralInfoBaseModelBuilder>, GeneralInfoBaseModelBuilder>();
            services.AddScoped<IBuilder<AddressBookModel, AddressBookModelBuilder>, AddressBookModelBuilder>();
            services.AddScoped<IBuilder<InvoiceLineBaseModel<InvoiceLineTaxBaseModel>, InvoiceLineBaseModelBuilder>, InvoiceLineBaseModelBuilder>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }

    [CollectionDefinition("eBelge")]
    public class StartupCollection : ICollectionFixture<StartupFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
