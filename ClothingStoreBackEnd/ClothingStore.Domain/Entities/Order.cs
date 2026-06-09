using ClothingStore.Domain.Common;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities
{
    //TODO
    public class Order : EntityBased
    {
        private readonly List<OrderItem> _items = new();

        private Order() { }
        public string OrderNumber { get; private set; } = "ORD-000001";
        public long UserId { get; private set; }// CustomerId 
        public decimal TotalAmount { get; private set; }

        public OrderStatus Status { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }

        //Shipping Address Snapshot
        public string? RecipientName { get; private set; } = null;
        public string? PhoneNumber { get; private set; } = null;
        public string? City { get; private set; } = null;
        public string? AddressLine { get; private set; } = null;


        public decimal ShippingCost { get; private set; }

        public decimal DiscountAmount { get; private set; }

        public string? CancellationReason { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();


        
        public Order(
        long userId,
        string recipientName,
        string phoneNumber,
        string city,
        string addressLine,
        decimal shippingCost = 0,
        decimal discountAmount = 0)
        {

            if (userId <= 0)
                throw new ArgumentException(nameof(userId));

            RecipientName = recipientName;
            PhoneNumber = phoneNumber;
            City = city;
            AddressLine = addressLine;

            ShippingCost = shippingCost;
            DiscountAmount = discountAmount;

            Status = OrderStatus.Pending;
            PaymentStatus = PaymentStatus.Pending;

        }


        public void SetOrderNumber(string orderNumber)
        {
            if(string.IsNullOrWhiteSpace(orderNumber))
                throw new ArgumentException(nameof(orderNumber));

            if(!string.IsNullOrWhiteSpace(OrderNumber))
                throw new InvalidOperationException("Order number already assigned.");

            OrderNumber = orderNumber;
        }


        public void AddItem(
        long productId,
        long variantId,
        string productName,
        decimal unitPrice,
        
        int quantity)
        {
            EnsureEditable();

            var item = OrderItem.Create(
                productId,
                variantId,
                productName,
                unitPrice,
                quantity);

            _items.Add(item);

            RecalculateTotal();
        }


        public void MarkAsPaid()
        {
            if (PaymentStatus == PaymentStatus.Paid)
                return; 
            PaymentStatus = PaymentStatus.Paid;
            MarkAsUpdated();
        }

        public void MarkAsFailed()
        {
            PaymentStatus = PaymentStatus.Failed;

            MarkAsUpdated();
        }

        public Result MarkAsProcessing()
        {
            if (Status != OrderStatus.Pending)
                return Result.Failure("Only pending orders can be processed.");

            Status = OrderStatus.Processing;
            MarkAsUpdated();
            return Result.Success();
        }
        public Result MarkAsShipped()
        {
            if (Status != OrderStatus.Processing)
                return Result.Failure(
                    "Order must be processing first.");

            Status = OrderStatus.Shipped;

            MarkAsUpdated();
            return Result.Success();
        }

        public Result MarkAsDelivered()
        {
            if (Status != OrderStatus.Shipped)
                return Result.Failure(
                    "Order must be shipped first.");

            Status = OrderStatus.Delivered;

            MarkAsUpdated();
            return Result.Success();
        }

        public Result Cancel(string reason)
        {
            if (Status == OrderStatus.Shipped ||
                Status == OrderStatus.Delivered)
            {
                return Result.Failure(
                    "Order can no longer be cancelled.");
            }

            if (string.IsNullOrWhiteSpace(reason))
                return Result.Failure(nameof(reason));

            Status = OrderStatus.Cancelled;
            CancellationReason = reason;
            MarkAsUpdated();
            return Result.Success();
        }


        public Result Validate()
        {
            if (!_items.Any())
                return Result.Failure(
                    "Order must contain at least one item.");

            return Result.Success();
        }


        private void RecalculateTotal()
        {
            TotalAmount =
                _items.Sum(x => x.SubTotal)
                + ShippingCost
                - DiscountAmount;
        }
    
    
        private Result EnsureEditable()
        {
            if (Status == OrderStatus.Shipped ||
          Status == OrderStatus.Delivered ||
          Status == OrderStatus.Cancelled)
            {
                return Result.Failure(
                    "Order can no longer be modified.");
            }


            return Result.Success();

        }
    }

}
