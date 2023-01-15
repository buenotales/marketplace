using Mkt.Business.ReportService.Application.Kafka;
using Mkt.Business.ReportService.Application.Services;
using Mkt.Business.ReportServiceProfile.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<KafkaClient, KafkaClient>();
        services.AddSingleton<ReportServiceProfileService, ReportServiceProfileService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
