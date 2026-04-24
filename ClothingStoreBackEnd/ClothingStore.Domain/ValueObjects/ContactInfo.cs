
namespace ClothingStore.Domain.ValueObjects
{
    public record ContactInfo
    {
        public string? Email { get; init; }
        public string? Address { get; init; }
        public string? PhoneNumber { get; init; }

        public ContactInfo(string? email, string? address, string? phoneNumber)
        {
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }

    }
}
