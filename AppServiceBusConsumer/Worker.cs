using AppServiceBusConsumer.Models;
using System.Text.Json;

namespace AppServiceBusConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly string queueName = "client";
    private readonly string connectionString;
    private ServiceBusClient busClient;
    private ServiceBusReceiver busReceiver;
    public Worker(
            ILogger<Worker> logger,
            IConfiguration configuration
        )
    {
        _logger = logger;
        _configuration = configuration;
        connectionString = _configuration.GetValue<string>("AzureServiceBus");

        busClient = new ServiceBusClient(connectionString);
        busReceiver = busClient.CreateReceiver(queueName);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Run(() =>
            {                
                MessagesConsumer();
            });
        }
    }

    private async Task MessagesConsumer()
    {                
        var receivedMessage = await busReceiver.ReceiveMessageAsync();

        if (receivedMessage != null)
        {
            string body = receivedMessage.Body.ToString();

            if (body != null)
            {
                var obj = JsonSerializer.Deserialize<Client>(body);
                Console.WriteLine(body);
                await busReceiver.CompleteMessageAsync(receivedMessage);
            }
        }        
    }
    
    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await busReceiver.CloseAsync();
        await busClient.DisposeAsync();

        _logger.LogInformation(
            "Conexao com o Azure Service Bus fechada!");
    }    
}
