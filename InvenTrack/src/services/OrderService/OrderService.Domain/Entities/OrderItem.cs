namespace OrderService.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    // Product is owned by ProductService.
    // This is only a logical reference.
    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    // Price snapshot at the time the order is created.
    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice =>
        UnitPrice * Quantity;

    private OrderItem()
    {
    }

    public OrderItem(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        Validate(productId, quantity, unitPrice);

        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    private static void Validate(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException(
                "ProductId is required.");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException(
                "Order quantity must be greater than zero.");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentException(
                "Unit price cannot be negative.");
        }
    }
}