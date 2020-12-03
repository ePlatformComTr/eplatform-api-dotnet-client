using ePlatform.Api.Core.Http;
using ePlatform.Api.eBelge.Invoice.Models;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;

namespace ePlatform.Api.eBelge.Invoice
{
    public class CommonClient
    {
        private readonly IFlurlClient flurlClient;
        
        public CommonClient(ClientOptions clientOptions, IFlurlClientFactory flurlClientFac)
        {
            flurlClient = flurlClientFac.Get(clientOptions.InvoiceServiceUrl).SetDefaultSettings();
            flurlClient.Headers["Client-Version"] = Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Http Code 200 means is user else is not user  eFatura=1,eIrsaliye=3
        /// </summary>
        public async Task<bool> IsUser(string identifier, int appType)
        {
            var response = await flurlClient.Request($"/v1/gibuser/isuser")
                .SetQueryParam("identifier", identifier)
                .SetQueryParam("appType", appType)
                .GetAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<GibUserWithAliasModel> GetUser(string identifier)
        {
            return await flurlClient.Request($"/v1/gibuser/getuser")
                .SetQueryParam("identifier", identifier)
                .GetJsonAsync<GibUserWithAliasModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<GibUserAliasModel>> GetUserAliasListZip()
        {
            using (var zipArcihve = new ZipArchive(await flurlClient.Request($"/v1/gibuser/receiverboxalias/zip").GetStreamAsync()))
            using (var entry = zipArcihve.Entries[0].Open())
            using (var sr = new StreamReader(entry))
            using (var reader = new JsonTextReader(sr))
            {
               return new JsonSerializer().Deserialize<List<GibUserAliasModel>>(reader);
            }
        }

        public async Task<List<CurrencyModel>> CurrencyCodeList()
        {
            return await flurlClient.Request($"/v1/staticlist/currency")
                .GetJsonAsync<List<CurrencyModel>>();
        }

        public async Task<List<UnitCodeModel>> UnitCodeList()
        {
            return await flurlClient.Request($"/v1/staticlist/unit")
                .GetJsonAsync<List<UnitCodeModel>>();
        }

        public async Task<List<TaxExemptionReasonModel>> TaxExemptionReasonList()
        {
            return await flurlClient.Request($"/v1/staticlist/taxexemptionreason")
                .GetJsonAsync<List<TaxExemptionReasonModel>>();
        }

        public async Task<List<WithHoldingCodeModel>> WithHoldingList()
        {
            return await flurlClient.Request($"/v1/staticlist/withholding")
                .GetJsonAsync<List<WithHoldingCodeModel>>();
        }

        public async Task<List<TaxTypeCodeModel>> TaxTypeCodeList()
        {
            return await flurlClient.Request($"/v1/staticlist/taxtypecode")
                .GetJsonAsync<List<TaxTypeCodeModel>>();
        }

        public async Task<List<TaxOfficeModel>> TaxOfficeList()
        {
            return await flurlClient.Request($"/v1/staticlist/taxoffice")
                .GetJsonAsync<List<TaxOfficeModel>>();
        }

        public async Task<List<CountryModel>> CountrList()
        {
            return await flurlClient.Request($"/v1/staticlist/country")
                .GetJsonAsync<List<CountryModel>>();
        }
    }
}
