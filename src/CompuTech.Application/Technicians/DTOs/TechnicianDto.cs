namespace CompuTech.Application.Technicians.DTOs;

public record TechnicianDto(
    int Id,
    string FullName,
    string Email,
    string Phone,
    string Specialty,
    bool IsActive,
    DateTime CreatedAt);
