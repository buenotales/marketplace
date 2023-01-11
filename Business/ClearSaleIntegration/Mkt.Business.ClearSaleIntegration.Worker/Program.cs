using Mkt.Business.ClearSaleIntegration.Application.Kafka;
using Mkt.Business.ClearSaleIntegration.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<KafkaClient, KafkaClient>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
