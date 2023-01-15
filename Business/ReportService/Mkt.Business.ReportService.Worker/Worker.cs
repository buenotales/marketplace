using Mkt.Business.ReportService.Application.Kafka;
using Mkt.Business.ReportService.Application.Services;
using System.Text.Json;

namespace Mkt.Business.ReportServiceProfile.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly KafkaClient kafkaClient;
        private readonly ReportServiceProfileService reportProfileService;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger, KafkaClient kafkaClient, ReportServiceProfileService reportProfileService, IConfiguration configuration)
        {
            this.logger = logger;
            this.kafkaClient = kafkaClient;
            this.reportProfileService = reportProfileService;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                kafkaClient.OpenConnection(configuration["Kafka:BatchReportAllOrdersByUserTopic"]);

                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    var consumeResult = kafkaClient.ConsumeEvent();

                    if (consumeResult != null)
                    {
                        var topicDestination = consumeResult.Message.Key.ToString();
                        logger.LogInformation("Mensagem consumida com sucesso, topic: {0}", JsonSerializer.Serialize(topicDestination));

                        var profiles = await reportProfileService.ListAllProfilesAsync();

                        if (profiles != null)
                        {
                            foreach (var profile in profiles.Items)
                            {
                                await kafkaClient.PublishEventAsync(topicDestination, profile.ProfileId.ToString());
                            }
                            kafkaClient.Commit(consumeResult);
                        }
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
}