namespace AppServiceBusConsumer.Models;

public class Client
{
    public Guid Id { get; }
    public string Name { get; set; }    
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Birtiday { get; set; }

    public List<Address> Adresses { get; set; }

    public Client()
        => Id = Guid.NewGuid();    
}
