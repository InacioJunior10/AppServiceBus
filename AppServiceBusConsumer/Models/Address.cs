namespace AppServiceBusConsumer.Models;

public class Address
{
    public Guid Id { get; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public AddressType AddressType { get; set; }

    public Guid IdClient { get; private set; }

    public Address()
        => Id = Guid.NewGuid();

    public void SetIdClient(Guid id)
        => IdClient = id;
    
}
