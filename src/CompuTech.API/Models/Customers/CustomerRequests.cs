namespace CompuTech.API.Models.Customers;

/// <summary>Request body for creating a new customer.</summary>
public record CreateCustomerRequest(string FullName, string Email, string Phone, string? TaxId);

/// <summary>Request body for updating an existing customer.</summary>
public record UpdateCustomerRequest(string FullName, string Phone, string? TaxId);

/// <summary>Request body for creating a new customer location.</summary>
public record CreateCustomerLocationRequest(string Name, string Address, string City, string? ContactPhone);

/// <summary>Request body for updating an existing customer location.</summary>
public record UpdateCustomerLocationRequest(string Name, string Address, string City, string? ContactPhone);
