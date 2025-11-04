namespace Grbus.WebShop.Application.Customers.DTOs
{
    public record CustomerDto
    {
        public required string Email { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }
}
