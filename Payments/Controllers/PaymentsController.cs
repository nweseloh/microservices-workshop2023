using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payments.Models;

namespace Payments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private ServiceBusOptions _options;

        public PaymentsController(IOptions<ServiceBusOptions> options)
        {
            _options = options.Value;
        }

        [HttpPost]
        public async Task Create(Payment payment)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient(_options.ConnectionString);

            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(_options.Topic);

            //var message = new ServiceBusMessage("PaymentCreated");

            var message = new ServiceBusMessage
            {
                Subject = "PaymentCreated",
                Body = new BinaryData(payment)
            };

            await serviceBusSender.SendMessageAsync(message);
        }
    }
}