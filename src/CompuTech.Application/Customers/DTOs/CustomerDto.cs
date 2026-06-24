namespace CompuTech.Application.Customers.DTOs;

public record CustomerDto(
    int Id,
    string FullName,
    string Email,
    string Phone,
    string? TaxId,
    bool IsActive,
    DateTime CreatedAt,
    List<CustomerLocationDto> Locations);
