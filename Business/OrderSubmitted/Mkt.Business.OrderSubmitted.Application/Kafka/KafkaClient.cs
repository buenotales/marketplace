using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Mkt.Business.OrderSubmitted.Application.Kafka
{
    public class KafkaClient
    {
        private readonly ILogger<KafkaClient> logger;
        private readonly IConfiguration configuration;

        public KafkaClient(ILogger<KafkaClient> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<bool> PublishEventAsync(string topic, string key, object value)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:Server"],
                ClientId = Dns.GetHostName()
            };

            try
            {
                using var producer = new ProducerBuilder<string, string>(config).Build();

                var result = await producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = JsonSerializer.Serialize(value) });

                logger.LogInformation("Evento publicado com sucesso, partição: {0}", result.Partition);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogCritical(JsonSerializer.Serialize(ex));
            }

            return false;
        }
    }
}
