using Microsoft.Extensions.Configuration;
using Mkt.Business.OrderSubmitted.Application.Dto.Request;
using Mkt.Business.OrderSubmitted.Application.Dto.Response;
using Mkt.Business.OrderSubmitted.Application.Kafka;
using System.Text;
using System.Text.Json;

namespace Mkt.Business.OrderSubmitted.Application.Services
{
    public class ManagmentService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly KafkaClient kafkaClient;
        private readonly IConfiguration configuration;

        public ManagmentService(KafkaClient kafkaClient, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.kafkaClient = kafkaClient;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<RegisterPostResponseDto> RegisterAsync(RegisterPostRequestDto requestDto)
        {
            using var httpClient = httpClientFactory.CreateClient();

            var url = configuration["Order:UrlBase"] + configuration["Order:RegisterEndpoint"];
            var body = new StringContent(JsonSerializer.Serialize(requestDto), Encoding.UTF8, "application/json");

            using var httpResponse = await httpClient.PostAsync(url, body);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseObj = JsonSerializer.Deserialize<RegisterPostResponseDto>(await httpResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                requestDto.OrderId = responseObj.OrderId;

                await kafkaClient.PublishEventAsync(configuration["kafka:NewOrderTopic"], requestDto.Profile.Email, requestDto);

                return new RegisterPostResponseDto() { OrderId = requestDto.OrderId, Message = "Pedido salvo. Pagamento em validação!" };
            }

            return null;
        }
    }
}
