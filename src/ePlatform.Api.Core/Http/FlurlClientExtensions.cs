using Flurl.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePlatform.Api.Core.Http
{
    public static class FlurlClientExtensions
    {
        public static T AcceptJsonHeader<T>(this T clientOrRequest) where T : IHttpSettingsContainer
        {
            return clientOrRequest.WithHeader("Accept", "application/json");
        }

        public async static Task<T> GetJsonWithDefaultAsync<T>(this IFlurlRequest request, System.Net.HttpStatusCode httpStatusCode)
        {
            try
            {
                return await request.GetJsonAsync<T>();

            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == httpStatusCode)
                {
                    return default;
                }
                throw;
            }
        }

        public static IFlurlClient SetDefaultSettings(this IFlurlClient flurlClient)
        {
            return flurlClient
                .AcceptJsonHeader()
                .HandleHttpStatusAsync();
        }

        public static IFlurlClient HandleHttpStatusAsync(this IFlurlClient flurlClient)
        {
            return flurlClient.Configure(config =>
            {
                config.OnErrorAsync = async httpCall =>
                {
                    httpCall.ExceptionHandled = true;
                    var correlationId = httpCall.Response.GetHeaderValue("X-Correlation-Id");

                    if ((int)httpCall.HttpStatus == 422)
                    {
                        var result = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(await httpCall.Response.Content.ReadAsStringAsync());
                        if (result.Any())
                        {
                            throw new EntityValidationException(result, result.FirstOrDefault().Value.FirstOrDefault(), correlationId, httpCall.Exception);
                        }
                    }
                    else if (httpCall.HttpStatus == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new EntityNotFoundException(await httpCall.Response.Content.ReadAsStringAsync(), correlationId, httpCall.Exception);
                    }
                    else if (httpCall.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                    {
                        throw new ForbiddenExcepitons(await httpCall.Response.Content.ReadAsStringAsync(), httpCall.Exception);
                    }
                    else if (httpCall.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedExcepiton(await httpCall.Response.Content.ReadAsStringAsync(), httpCall.Exception);
                    }
                    else
                    {
                        throw new ePlatformException(await httpCall.Response.Content.ReadAsStringAsync(), correlationId, httpCall.Exception);
                    }
                };
            });
        }
    }
}
