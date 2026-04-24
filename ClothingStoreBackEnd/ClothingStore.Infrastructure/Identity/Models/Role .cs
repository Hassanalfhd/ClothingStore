
using Microsoft.AspNetCore.Identity;

namespace ClothingStore.Identity.Models;
public class Role : IdentityRole<long>
{
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Role() { }

    public Role(string roleName, string description) : base(roleName)
    {
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }


    public void UpdateDescription(string description)
    {
        Description = description;
    }
}


