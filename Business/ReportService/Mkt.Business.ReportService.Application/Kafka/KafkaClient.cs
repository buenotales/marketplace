using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Mkt.Business.ReportService.Application.Kafka
{
    public class KafkaClient
    {
        private readonly ILogger<KafkaClient> logger;
        private readonly IConfiguration configuration;
        private IConsumer<Ignore, string> consumerBuilder;

        public KafkaClient(ILogger<KafkaClient> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        ~KafkaClient()
        {
            CloseConnection();
        }

        public void OpenConnection(string topic)
        {
            if (consumerBuilder == null)
            {
                var config = new ConsumerConfig
                {
                    GroupId = configuration["Kafka:GroupId"],
                    BootstrapServers = configuration["Kafka:Server"],
                    ClientId = configuration["Kafka:GroupId"] + "-" + Guid.NewGuid(),
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false,
                    Acks = Acks.All
                };

                consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build();
                consumerBuilder.Subscribe(topic);
            }
        }

        public void CloseConnection()
        {
            if (consumerBuilder != null)
                consumerBuilder.Close();
        }

        public void Commit(ConsumeResult<Ignore, string> consumer)
        {
            try
            {
                consumerBuilder.Commit(consumer);
            }
            catch (KafkaException e)
            {
                logger.LogCritical($"Commit error: {e.Error.Reason}");
            }
        }

        public ConsumeResult<Ignore, string> ConsumeEvent()
        {
            try
            {
                //return consumerBuilder.Consume(TimeSpan.FromSeconds(int.Parse(configuration["Kafka:Timeout"])));
                return consumerBuilder.Consume();
            }
            catch (Exception ex)
            {
                logger.LogCritical(JsonSerializer.Serialize(ex));
                return null;
            }
        }

        public async Task<bool> PublishEventAsync(string topic, string key)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:Server"],
                ClientId = Dns.GetHostName()
            };

            try
            {
                using var producer = new ProducerBuilder<string, string>(config).Build();

                var result = await producer.ProduceAsync(topic, new Message<string, string> { Key = key });

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
