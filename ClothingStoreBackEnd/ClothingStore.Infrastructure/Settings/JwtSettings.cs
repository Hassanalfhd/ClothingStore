using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Infrastructure.Settings
{
    public sealed class JwtSettings
    {
        public string SecretKey { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int AccessTokenMinutes { get; init; }
        public int RefreshTokenDays { get; init; }
    }
}
