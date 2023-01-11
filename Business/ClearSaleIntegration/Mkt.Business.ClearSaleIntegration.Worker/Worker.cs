using Mkt.Business.ClearSaleIntegration.Application.Kafka;
using Mkt.Business.ClearSaleIntegration.Application.Messages;
using System.Text.Json;

namespace Mkt.Business.ClearSaleIntegration.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IConfiguration configuration;
        private readonly KafkaClient kafkaClient;

        public Worker(ILogger<Worker> logger, KafkaClient kafkaClient, IConfiguration configuration)
        {
            this.logger = logger;
            this.kafkaClient = kafkaClient;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            kafkaClient.OpenConnection(configuration["Kafka:NewOrderTopic"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var consumeResult = kafkaClient.ConsumeEvent();

                if (consumeResult != null)
                {
                    var message = JsonSerializer.Deserialize<NewOrderMessage>(consumeResult.Message.Value);
                    logger.LogInformation("Mensagem consumida com sucesso, message: {0}", JsonSerializer.Serialize(message));
                    kafkaClient.Commit(consumeResult);
                }
                else
                {
                    logger.LogWarning("Não foi encontrado nenhuma mensagem");
                }

                logger.LogInformation("Worker sleeping at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            kafkaClient.CloseConnection();
        }
    }
}