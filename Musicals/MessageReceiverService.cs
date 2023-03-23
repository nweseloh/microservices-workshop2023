using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Musicals.UseCases;

namespace Musicals;

public class MessageReceiverService : IHostedService
{
    private readonly ILogger<MessageReceiverService> _logger;
    private readonly IReservationUseCase _reservationUseCase;
    private readonly ServiceBusOptions _options;
    private ServiceBusProcessor? _processor;

    public MessageReceiverService(
        ILogger<MessageReceiverService> logger,
        IReservationUseCase reservationUseCase,
        IOptions<ServiceBusOptions> options)
    {
        _logger = logger;
        _reservationUseCase = reservationUseCase;
        _options = options.Value;
    }
   
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var client = new ServiceBusClient(_options.ConnectionString);
       
        _processor = client.CreateProcessor(_options.Topic, "musicals");

        _processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
        _processor.ProcessMessageAsync += Processor_ProcessMessageAsync;

        await _processor.StartProcessingAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_processor != null)
        {
            await _processor.StopProcessingAsync(cancellationToken);

            _processor.ProcessErrorAsync -= Processor_ProcessErrorAsync;
            _processor.ProcessMessageAsync -= Processor_ProcessMessageAsync;
        }
    }

    private Task Processor_ProcessMessageAsync(ProcessMessageEventArgs arg)
    {
        ServiceBusReceivedMessage? message = arg.Message;
        if (message.Subject == "PaymentCreated")
        {
            var paymentCreated = JsonSerializer.Deserialize<PaymentCreated>(message.Body);

            _reservationUseCase.ConfirmReservation(paymentCreated.ReservationId);
        }
        else
            throw new NotSupportedException("Invalid subject");

        return Task.CompletedTask;
    }

    private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
    {
       _logger.LogError(arg.Exception, "Process error:");

       return Task.CompletedTask;
    }

    private class PaymentCreated
    {
        public int ReservationId { get; set; }
    }
}