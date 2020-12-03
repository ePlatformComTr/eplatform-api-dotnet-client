using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePlatform.Api.eBelge.Ticket.Common;
using ePlatform.Api.eBelge.Ticket.Tests.Setup;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Ticket.Tests.Functional
{
    [Collection("ticket-startup")]
    public class CommonTicketClientTest
    {
        private readonly CommonTicketClient _commonTicketClient;

        public CommonTicketClientTest(StartupFixture fixture)
        {
            _commonTicketClient = fixture.ServiceProvider.GetRequiredService<CommonTicketClient>();
        }
        
        [Fact]
        public async Task Should_Return_City_List()
        {
            var result = await _commonTicketClient.GetCityList();
            var cityIds = new List<long>
            {
                1,
                34,
                81
            };
            Assert.NotNull(result);
            Assert.Equal(cityIds.Count, result.Select(p => p.Id).Intersect(cityIds).Count());
        }

    }
}
