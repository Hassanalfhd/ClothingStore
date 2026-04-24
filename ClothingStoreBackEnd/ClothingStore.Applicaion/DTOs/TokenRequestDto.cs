
namespace ClothingStore.Application.DTOs
{
    public class TokenRequestDto
    {
        public string publicId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

    }
}
