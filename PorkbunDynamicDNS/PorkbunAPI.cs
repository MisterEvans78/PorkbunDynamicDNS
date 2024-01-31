using PorkbunDynamicDNS.Models;
using System.Net.Http.Json;

namespace PorkbunDynamicDNS
{
    public class PorkbunAPI
    {
        private HttpClient _client;

        public PorkbunAPI()
        {
            _client = new()
            {
                BaseAddress = new Uri(ConfigurationManager.Get("API:Endpoint"))
            };
        }

        public async Task<string> GetIP()
        {
            PorkbunApiBody body = new()
            {
                ApiKey = ConfigurationManager.Get("API:ApiKey"),
                SecretApiKey = ConfigurationManager.Get("API:SecretApiKey")
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("ping", body);

            if (response.IsSuccessStatusCode)
            {
                PorkbunApiResponse? apiResponse = await response.Content.ReadFromJsonAsync<PorkbunApiResponse>();
                if (apiResponse?.YourIP is not null)
                {
                    return apiResponse.YourIP;
                }
                return string.Empty;
            }

            throw new Exception($"{(int)response.StatusCode} {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}");
        }

        public async Task<IEnumerable<Record>?> GetRecords(string domain)
        {
            return await RetrieveRecords(domain);
        }

        public async Task<Record?> GetRecordByID(string domain, int id)
        {
            return (await RetrieveRecords(domain, id))?.FirstOrDefault();
        }

        public async Task<int?> CreateRecord(string domain, string subdomain, string ip, int? ttl = null)
        {
            return (await CreateOrEditRecord(domain, subdomain, ip, ttl))?.Id;
        }

        public async Task EditRecord(string domain, string subdomain, string ip, int? ttl = null)
        {
            await CreateOrEditRecord(domain, subdomain, ip, ttl, true);
        }

        private async Task<IEnumerable<Record>?> RetrieveRecords(string domain, int? id = null)
        {
            PorkbunApiBody body = new()
            {
                ApiKey = ConfigurationManager.Get("API:ApiKey"),
                SecretApiKey = ConfigurationManager.Get("API:SecretApiKey")
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(id.HasValue ? $"dns/retrieve/{domain}/{id}" : $"dns/retrieve/{domain}", body);

            if (response.IsSuccessStatusCode)
            {
                PorkbunApiResponse? apiResponse = await response.Content.ReadFromJsonAsync<PorkbunApiResponse>();
                return apiResponse?.Records;
            }

            throw new Exception($"{(int)response.StatusCode} {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}");
        }

        private async Task<PorkbunApiResponse?> CreateOrEditRecord(string domain, string subdomain, string ip, int? ttl = null, bool update = false)
        {
            PorkbunApiBody body = new()
            {
                ApiKey = ConfigurationManager.Get("API:ApiKey"),
                SecretApiKey = ConfigurationManager.Get("API:SecretApiKey"),
                Name = !update ? subdomain : null,
                Type = !update ? "A" : null,
                Content = ip,
                Ttl = ttl.HasValue ? ttl.ToString() : null
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(update ? $"dns/editByNameType/{domain}/A/{subdomain}" : $"dns/create/{domain}", body);

            if (response.IsSuccessStatusCode)
            {
                PorkbunApiResponse? apiResponse = await response.Content.ReadFromJsonAsync<PorkbunApiResponse>();
                return apiResponse;
            }

            throw new Exception($"{(int)response.StatusCode} {response.StatusCode}. Response: {await response.Content.ReadAsStringAsync()}");
        }
    }
}
