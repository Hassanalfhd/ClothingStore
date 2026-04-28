
namespace ClothingStore.Domain.ValueObjects
{
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }
        
        public Money(decimal amount, string currency)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if(string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.");

            Amount = decimal.Round(amount, 2);
            Currency = currency.ToUpperInvariant();
        }


        public static Money Zero(string currency = "YER")
            => new(0, currency);

        public bool Equals(Money? other)
        {
            if (other == null) return false;

            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object? obj) 
            => obj is Money other && Equals(other);
        public override int GetHashCode()
            => HashCode.Combine(Amount, Currency);

        public override string ToString()
            => $"{Amount} {Currency}";

    }
}
