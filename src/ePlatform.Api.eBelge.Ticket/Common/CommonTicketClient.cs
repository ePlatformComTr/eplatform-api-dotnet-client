using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ePlatform.Api.Core.Http;
using ePlatform.Api.eBelge.Ticket.Common.Models;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace ePlatform.Api.eBelge.Ticket.Common
{
    public class CommonTicketClient
    {
        private readonly IFlurlClient flurlClient;

        public CommonTicketClient(ClientOptions clientOptions, IFlurlClientFactory flurlClientFactory)
        {
            flurlClient = flurlClientFactory.Get(clientOptions.TicketServiceUrl).SetDefaultSettings();
            flurlClient.Headers["Client-Version"] = Assembly.GetExecutingAssembly().GetName().Version;
        }

        public async Task<List<CityModel>> GetCityList()
        {
            return await flurlClient.Request($"/v1/city/list")
                .GetJsonAsync<List<CityModel>>();
        }
    }
}
