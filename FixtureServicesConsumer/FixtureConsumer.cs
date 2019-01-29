using Clarksons.Ops.FixtureContract.Get.v1;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace FixtureServiceConsumer
{
    public class FixtureConsumer : IDisposable
    {
        private readonly HttpClient _httpClient;

        public FixtureConsumer(string baseUri = null, string authToken = null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://my.api/v2/capture") };

            if (authToken != null)
            {
                _httpClient.DefaultRequestHeaders.Add("X-Clarksons-Security-Cloud", $"Bearer {authToken}");
            }
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public Fixture GetFixture(Guid id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1.0/fixture/{id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-Clarksons-Security-Cloud", $"{token}");

            var response = _httpClient.SendAsync(request);

            try
            {
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Fixture>(content, _jsonSettings);
                }

                RaiseResponseError(request, result);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        private static void RaiseResponseError(HttpRequestMessage failedRequest, HttpResponseMessage failedResponse)
        {
            throw new HttpRequestException(
                String.Format("The Events API request for {0} {1} failed. Response Status: {2}, Response Body: {3}",
                failedRequest.Method.ToString().ToUpperInvariant(),
                failedRequest.RequestUri,
                (int)failedResponse.StatusCode,
                failedResponse.Content.ReadAsStringAsync().Result));
        }

        public void Dispose()
        {
            Dispose(_httpClient);
        }

        public void Dispose(params IDisposable[] disposables)
        {
            foreach (var disposable in disposables.Where(d => d != null))
            {
                disposable.Dispose();
            }
        }
    }
}
