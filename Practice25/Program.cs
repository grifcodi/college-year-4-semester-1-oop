
Console.WriteLine("Bad code demonstration. Start.");

var badProcessor = new BadOrderProcessor();
badProcessor.AddItem("Keyboard", 1500m);
badProcessor.AddItem("Mouse", 800m);
badProcessor.ProcessOrder();

Console.WriteLine("Bad code demonstration. End.\n");

Console.WriteLine("SOLID code demonstration. Start.");

var order = new Order();
order.AddItem("Keyboard", 1500m);
order.AddItem("Mouse", 800m);

var processor = new OrderProcessor(
    new ThresholdDiscountPolicy(2000m, 0.10m),
    new ConsoleReportPrinter(),
    new FileOrderRepository("order.txt"),
    new ConsoleNotificationService());

processor.ProcessOrder(order);

Console.WriteLine("SOLID code demonstration. End.");

class BadOrderProcessor
{
    // Порушення SRP: один клас виконує кілька обов'язків.
    private readonly List<(string Name, decimal Price)> _items = new();

    public void AddItem(string name, decimal price)
        => _items.Add((name, price));

    public void ProcessOrder()
    {
        // Порушення OCP: правило знижки жорстко задане в коді.
        decimal subtotal = _items.Sum(x => x.Price);
        decimal discount = subtotal >= 2000m ? 0.10m : 0m;
        decimal total = subtotal * (1 - discount);

        Console.WriteLine("ORDER REPORT");
        foreach (var item in _items)
            Console.WriteLine($"Product: {item.Name} — {item.Price} UAH");

        Console.WriteLine($"\nSubtotal: {subtotal} UAH");
        Console.WriteLine($"Discount: {discount:P0}");
        Console.WriteLine($"Total: {total} UAH");

        // Порушення DIP: клас напряму залежить від файлової системи.
        File.WriteAllText("bad-order.txt", $"Subtotal: {subtotal}, Total: {total}");

        Console.WriteLine("\nOrder saved.");
        Console.WriteLine("Notification sent (simulation).");
    }
}

// Рефакторинг коду

public class Item(string name, decimal price)
{
    public string Name { get; } = name;
    public decimal Price { get; } = price;
}

public class Order
{
    private readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items => _items.AsReadOnly();
    public void AddItem(string name, decimal price) => _items.Add(new Item(name, price));
    public decimal Subtotal => _items.Sum(x => x.Price);
}

//SRP: OrderProcessor більше не виконує всі операції самостійно - розрахунок, звіт, збереження та повідомлення розділено між класами.

public record OrderResult(decimal Subtotal, decimal DiscountRate, decimal Total);

public interface IDiscountPolicy
{
    OrderResult Calculate(Order order);
}

public interface IReportPrinter
{
    void Print(Order order, OrderResult result);
}

public interface IOrderRepository
{
    void Save(OrderResult result);
}

public interface INotificationService
{
    void Notify(string message);
}

public class ThresholdDiscountPolicy(decimal threshold, decimal rate) : IDiscountPolicy
{
    public OrderResult Calculate(Order order)
    {
        decimal subtotal = order.Subtotal;
        decimal discount = subtotal >= threshold ? rate : 0m;
        return new OrderResult(subtotal, discount, subtotal * (1 - discount));
    }
}

public class ConsoleReportPrinter : IReportPrinter
{
    public void Print(Order order, OrderResult result)
    {
        Console.WriteLine("ORDER REPORT");
        foreach (var item in order.Items)
            Console.WriteLine($"Product: {item.Name} — {item.Price} UAH");

        Console.WriteLine($"\nSubtotal: {result.Subtotal} UAH");
        Console.WriteLine($"Discount: {result.DiscountRate:P0}");
        Console.WriteLine($"Total: {result.Total} UAH");
    }
}

public class FileOrderRepository(string filePath) : IOrderRepository
{
    public void Save(OrderResult result)
    {
        File.WriteAllText(filePath, $"Subtotal: {result.Subtotal}, Total: {result.Total}");
        Console.WriteLine("\nOrder saved.");
    }
}

public class ConsoleNotificationService : INotificationService
{
    public void Notify(string message) => Console.WriteLine(message);
}

public class OrderProcessor(
    IDiscountPolicy discountPolicy,
    IReportPrinter printer,
    IOrderRepository repository,
    INotificationService notifications)
{
    public void ProcessOrder(Order order)
    {
        var result = discountPolicy.Calculate(order);
        printer.Print(order, result);
        repository.Save(result);
        notifications.Notify("Notification sent (simulation).");
    }
}
