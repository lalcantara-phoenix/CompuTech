namespace CompuTech.Domain.Entities;

public class Customer
{
    public int Id { get; private set; }
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string? TaxId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<CustomerLocation> Locations { get; private set; } = [];

    private Customer() { }

    public static Customer Create(string fullName, string email, string phone, string? taxId = null)
    {
        return new Customer
        {
            FullName = fullName,
            Email = email,
            Phone = phone,
            TaxId = taxId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string fullName, string phone, string? taxId)
    {
        FullName = fullName;
        Phone = phone;
        TaxId = taxId;
    }

    public void Deactivate() => IsActive = false;
}
