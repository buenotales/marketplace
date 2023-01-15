using Microsoft.AspNetCore.Mvc;
using Mkt.Business.ReportService.Application.Kafka;

namespace Mkt.Business.ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : ControllerBase
    {
        private readonly KafkaClient kafkaClient;
        private readonly IConfiguration configuration;

        public ReportServiceController(KafkaClient kafkaClient, IConfiguration configuration) : this(kafkaClient)
        {
            this.configuration = configuration;
        }

        public ReportServiceController(KafkaClient kafkaClient)
        {
            this.kafkaClient = kafkaClient;
        }

        [HttpPost("AllOrdersByUser")]
        public async Task<IActionResult> GenerateOrderToAllUsers()
        {
            var topic = configuration["Kafka:BatchReportAllOrdersByUserTopic"];
            var key = configuration["Kafka:BatchReportAllOrdersByUserKey"];

            await kafkaClient.PublishEventAsync(topic, key);

            return Ok();
        }
    }
}
