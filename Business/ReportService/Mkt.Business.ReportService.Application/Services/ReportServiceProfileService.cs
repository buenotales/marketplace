using Microsoft.Extensions.Configuration;
using Mkt.Business.ReportService.Application.Dto.Profile.Response;
using Mkt.Business.ReportService.Application.Kafka;
using System.Text.Json;

namespace Mkt.Business.ReportService.Application.Services
{
    public class ReportServiceProfileService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public ReportServiceProfileService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<ProfileGetResponseDto> ListAllProfilesAsync()
        {
            using var httpClient = httpClientFactory.CreateClient();

            var url = configuration["Profile:UrlBase"] + configuration["Profile:ListAllEndpoint"];

            using var httpResponse = await httpClient.GetAsync(url);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializer.Deserialize<ProfileGetResponseDto>(await httpResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return null;
        }
    }
}
