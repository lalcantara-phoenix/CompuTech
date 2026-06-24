namespace CompuTech.Application.Customers.DTOs;

public record CustomerLocationDto(
    int Id,
    int CustomerId,
    string Name,
    string Address,
    string City,
    string? ContactPhone,
    bool IsActive);
