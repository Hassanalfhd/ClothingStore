using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClothingStore.Domain.Entities
{
    public class RefreshToken : EntityBased
    {
        public long ApplicationUserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public bool IsRevoked { get; private set; }
        public bool IsUsed { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiryDate;

        public bool IsActive => !IsUsed && !IsRevoked && !IsExpired;

        protected RefreshToken() { }

        public RefreshToken(long userId, string token, int durationInMinutes)
        {

            ApplicationUserId = userId;
            Token = token;
            ExpiryDate = DateTime.UtcNow.AddMinutes(durationInMinutes);
            IsRevoked = false;
            IsUsed = false;
        }


        public void MarkAsUsed()
        {
            if (IsUsed) throw new InvalidOperationException("Token has already been used.");
            if (IsRevoked) throw new InvalidOperationException("Token is revoked.");
            if (IsExpired) throw new InvalidOperationException("Token has expired.");

            IsUsed = true;

            base.MarAsUpdated();
        }

        public void Revoke()
        {
            IsRevoked = true;
            base.MarAsUpdated();

        }
        public void ExtendExpiry(int additionalMinutes)
        {
            if (IsRevoked) throw new InvalidOperationException("Cannot extend a revoked token.");
            ExpiryDate = ExpiryDate.AddMinutes(additionalMinutes);

        }
    }
}
