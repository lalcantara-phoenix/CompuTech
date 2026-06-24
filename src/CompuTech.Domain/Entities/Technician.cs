namespace CompuTech.Domain.Entities;

public class Technician
{
    public int Id { get; private set; }
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Specialty { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Technician() { }

    public static Technician Create(string fullName, string email, string phone, string specialty)
    {
        return new Technician
        {
            FullName = fullName,
            Email = email,
            Phone = phone,
            Specialty = specialty,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string fullName, string phone, string specialty)
    {
        FullName = fullName;
        Phone = phone;
        Specialty = specialty;
    }

    public void Deactivate() => IsActive = false;
}
