using System.Text;

namespace AppServiceBus.Services;

public class SendMessageQueue : ISendMessageQueue
{
    private readonly IConfiguration _configuration;
    private readonly string connectionString;
    
    public SendMessageQueue(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = _configuration.GetValue<string>("AzureServiceBus");
    }
    
    public async Task<bool> Send(object obj, string queueName)
    {
        try
        {            
            await using var queueClient = new ServiceBusClient(connectionString);

            var sender = queueClient.CreateSender(queueName);
            string messageBody = JsonSerializer.Serialize(obj);
            var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

            await sender.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        return true;
    }
}
