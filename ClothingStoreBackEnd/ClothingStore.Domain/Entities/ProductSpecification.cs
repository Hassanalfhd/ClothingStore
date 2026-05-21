
namespace ClothingStore.Domain.Entities
{
    public class ProductSpecification
    {
        public long Id { get; private set; }
        public long ProductId { get; private set; }

        public string Key { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;

        public Product Product { get; private set; } = null!;

        private ProductSpecification() { }  

        public ProductSpecification(long productId, string key, string value)
        {
            ProductId = productId;
            SetKey(key);
            SetValue(value);

        }

        public void SetKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Specification value is required.");

            Key = key.Trim();
        }

        public void SetValue(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Specification value is required.");

            Value = value.Trim();
        }
    }
}
