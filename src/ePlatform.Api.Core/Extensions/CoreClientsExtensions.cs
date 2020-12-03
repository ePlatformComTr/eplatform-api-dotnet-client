using System.Net.Http.Headers;
using ePlatform.Api.Core.Auth;
using ePlatform.Api.Core.Http;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CoreClientsExtensions
    {
        public static void AddCoreClients(this IServiceCollection services)
        {
            services.AddHttpClient<AuthClient>(client =>
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<IFlurlClientFactory>(serviceProvider =>
            {
                var auth = serviceProvider.GetService<AuthClient>();
                FlurlHttp.Configure(settings => settings.HttpClientFactory = new PollyHttpClientFactory(auth));
                return new PerBaseUrlFlurlClientFactory();

            });
        }
        
        private const string ePlatformClientOptionsSectionName = "ePlatformClientOptions";
        public static void ConfigureAndAddClientOptions(this IServiceCollection services,IConfiguration configuration,
            string sectionName = ePlatformClientOptionsSectionName)
        {
            var section = configuration.GetSection(sectionName);
            services.Configure<ClientOptions>(section);
            var options = new ClientOptions();
            section.Bind(options);
            services.AddSingleton(options);
        }
        
        public static void ConfigureCoreClientOptions(this IServiceCollection services,ClientOptions options)
        {
            services.Configure<ClientOptions>(opt =>
            {
                opt.Auth.Username = options.Auth.Username;
                opt.Auth.Password = options.Auth.Password;
                opt.Auth.ClientId = options.Auth.ClientId;
                opt.AuthServiceUrl = options.AuthServiceUrl;
            });
        }
    }
}
