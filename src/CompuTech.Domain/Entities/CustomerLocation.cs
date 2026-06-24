namespace CompuTech.Domain.Entities;

public class CustomerLocation
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string? ContactPhone { get; private set; }
    public bool IsActive { get; private set; }

    public Customer Customer { get; private set; } = default!;

    private CustomerLocation() { }

    public static CustomerLocation Create(int customerId, string name, string address, string city, string? contactPhone = null)
    {
        return new CustomerLocation
        {
            CustomerId = customerId,
            Name = name,
            Address = address,
            City = city,
            ContactPhone = contactPhone,
            IsActive = true
        };
    }

    public void Update(string name, string address, string city, string? contactPhone)
    {
        Name = name;
        Address = address;
        City = city;
        ContactPhone = contactPhone;
    }

    public void Deactivate() => IsActive = false;
}
