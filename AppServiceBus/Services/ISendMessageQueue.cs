namespace AppServiceBus.Services
{
    public interface ISendMessageQueue
    {
        Task<bool> Send(object obj, string queueName);
    }
}
