namespace OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }

    // User is owned by Identity/Auth service.
    // This is only a logical reference.
    public Guid UserId { get; private set; }

    public OrderStatus Status { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items =>
        _items.AsReadOnly();

    private Order()
    {
    }

    public Order(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                "UserId is required.");
        }

        Id = Guid.NewGuid();
        UserId = userId;
        Status = OrderStatus.Pending;
        TotalAmount = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(
        Guid productId,
        int quantity,
        decimal unitPrice)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(
                "Items can only be added to pending orders.");
        }

        var item = new OrderItem(
            productId,
            quantity,
            unitPrice);

        _items.Add(item);

        RecalculateTotal();
    }

    public void Confirm()
    {
        if (!_items.Any())
        {
            throw new InvalidOperationException(
                "An order must contain at least one item.");
        }

        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending orders can be confirmed.");
        }

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Order is already cancelled.");
        }

        if (Status == OrderStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "Confirmed orders cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }

    private void RecalculateTotal()
    {
        TotalAmount = _items.Sum(item => item.TotalPrice);
    }
}