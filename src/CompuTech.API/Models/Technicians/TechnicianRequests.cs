namespace CompuTech.API.Models.Technicians;

/// <summary>Request body for creating a new technician.</summary>
public record CreateTechnicianRequest(string FullName, string Email, string Phone, string Specialty);

/// <summary>Request body for updating an existing technician.</summary>
public record UpdateTechnicianRequest(string FullName, string Phone, string Specialty);
