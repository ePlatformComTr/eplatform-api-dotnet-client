using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ePlatform.Api.eBelge.Invoice.Tests.Functional
{
    [Collection("eBelge")]
    public class CommonClientTest
    {
        private readonly CommonClient _commonClient;
        public CommonClientTest(StartupFixture fixture)
        {
            _commonClient = fixture.ServiceProvider.GetRequiredService<CommonClient>();
        }

        [Fact]
        public async Task Should_Check_IsUser()
        {
            var result = await _commonClient.IsUser("1234567803", 1);
            Assert.True(result);
        }

        [Fact]
        public async Task Should_Get_User()
        {
            var result = await _commonClient.GetUser("1234567803");
            Assert.NotNull(result);
            Assert.NotNull(result.Definition);
            Assert.NotNull(result.ReceiverboxAliases);
            Assert.NotEmpty(result.ReceiverboxAliases);
            Assert.NotNull(result.SenderboxAliases);
            Assert.NotEmpty(result.SenderboxAliases);
            Assert.Equal("1234567803", result.Definition.Identifier);
        }

        [Fact]
        public async Task Should_Get_User_Alias_Zip()
        {
            var result = await _commonClient.GetUserAliasListZip();
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            foreach (var item in result.Where(x => !x.Alias.Contains("defaultpk")))
            {
                Assert.NotEmpty(item.Identifier);
                Assert.Contains(":", item.Alias);
                Assert.True(item.AppType == 1 || item.AppType == 3);
            }
        }

        [Fact]
        public async Task Should_Return_Currency_Code_List()
        {
            var result = await _commonClient.CurrencyCodeList();
            var currencyList = new List<string>
            {
                "CHF",
                "TRY",
                "EUR"
            };
            Assert.NotNull(result);
            Assert.Equal(currencyList.Count, result.Select(p => p.Code).Intersect(currencyList).Count());
        }

        [Fact]
        public async Task Should_Return_Unit_Code_List()
        {
            var result = await _commonClient.UnitCodeList();
            var unitCodeList = new List<string>
            {
                "C62",
                "WEE",
                "CMT"
            };
            Assert.NotNull(result);
            Assert.Equal(unitCodeList.Count, result.Select(p => p.Code).Intersect(unitCodeList).Count());
        }

        [Fact]
        public async Task Should_Return_Tax_Exemption_Reason_List()
        {
            var result = await _commonClient.TaxExemptionReasonList();
            var taxExemptionReasonList = new List<string>
            {
                "201",
                "227",
                "803"
            };
            Assert.NotNull(result);
            Assert.Equal(taxExemptionReasonList.Count, result.Select(p => p.Value).Intersect(taxExemptionReasonList).Count());
        }

        [Fact]
        public async Task Should_Return_Tax_Type_Code_List()
        {
            var result = await _commonClient.TaxTypeCodeList();
            var taxTypeCodeList = new List<string>
            {
                "0003",
                "0015",
                "8002"
            };
            Assert.NotNull(result);
            Assert.Equal(taxTypeCodeList.Count, result.Select(p => p.Code).Intersect(taxTypeCodeList).Count());
        }
        [Fact]
        public async Task Should_Return_Tax_Office_List()
        {
            var result = await _commonClient.TaxOfficeList();
            var taxOfficeList = new List<string>
            {
                "01205",
                "34203",
                "34263"
            };
            Assert.NotNull(result);
            Assert.Equal(taxOfficeList.Count, result.Select(p => p.Code).Intersect(taxOfficeList).Count());
        }
        [Fact]
        public async Task Should_Return_Country_List()
        {
            var result = await _commonClient.CountrList();
            var countryList = new List<string>
            {
                "Türkiye",
                "Brezilya",
                "Bulgaristan"
            };
            Assert.NotNull(result);
            Assert.Equal(countryList.Count, result.Select(p => p.Name).Intersect(countryList).Count());
        }
    }
}
