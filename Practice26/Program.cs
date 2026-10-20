DeliveryService service = DeliveryService.Instance;

var customer = new CustomerObserver("Іван");
var manager = new ManagerObserver("M-01");
service.Subscribe(customer);
service.Subscribe(manager);

Parcel parcel = ParcelFactory.CreateParcel("PKG-001", 3.0);
Console.WriteLine($"Служба доставки: {service.CompanyName}");
Console.WriteLine($"Посилка: {parcel.TrackingNumber}");
Console.WriteLine($"Вага: {parcel.Weight} кг");
Console.WriteLine($"Factory створила: {parcel.TypeName}");
Console.WriteLine($"Статус: {parcel.Status}");

IDeliveryStrategy expressStrategy = new ExpressDeliveryStrategy();
DeliveryService.ProcessPayment(parcel, expressStrategy);

service.UpdateParcelStatus(parcel, "У дорозі");

public interface IDeliveryStrategy
{
    string Name { get; }
    decimal CalculateCost(double weight);
}

public class StandardDeliveryStrategy : IDeliveryStrategy
{
    public string Name => "Стандартна доставка";
    public decimal CalculateCost(double weight) => 50 + (decimal)(weight * 10);
}

public class ExpressDeliveryStrategy : IDeliveryStrategy
{
    public string Name => "Експрес-доставка";
    public decimal CalculateCost(double weight) => 100 + (decimal)(weight * 25);
}

public abstract class Parcel(string trackingNumber, double weight)
{
    public string TrackingNumber { get; } = trackingNumber;
    public double Weight { get; } = weight;
    public string Status { get; private set; } = "Створено";

    public abstract string TypeName { get; }

    public void UpdateStatus(string newStatus)
    {
        Status = newStatus;
    }
}

public class SmallParcel(string trackingNumber, double weight) : Parcel(trackingNumber, weight)
{
    public override string TypeName => "SmallParcel";
}

public class HeavyParcel(string trackingNumber, double weight) : Parcel(trackingNumber, weight)
{
    public override string TypeName => "HeavyParcel";
}

public static class ParcelFactory
{
    public static Parcel CreateParcel(string trackingNumber, double weight)
    {
        if (weight <= 5.0)
            return new SmallParcel(trackingNumber, weight);
        else
            return new HeavyParcel(trackingNumber, weight);
    }
}

public interface IObserver
{
    void Update(string trackingNumber, string status);
}

public class CustomerObserver(string name) : IObserver
{
    private readonly string _name = name;

    public void Update(string trackingNumber, string status)
    {
        Console.WriteLine($"Observer повідомив клієнта {_name}: статус {trackingNumber} -> {status}");
    }
}

public class ManagerObserver(string managerId) : IObserver
{
    public void Update(string trackingNumber, string status)
    {
        Console.WriteLine($"Observer повідомив менеджера {managerId}: статус {trackingNumber} -> {status}");
    }
}

public sealed class DeliveryService
{
    private static DeliveryService? _instance;
    private static readonly Lock _lock = new();

    public string CompanyName { get; }
    private readonly List<IObserver> _observers = [];

    private DeliveryService()
    {
        CompanyName = "FastDelivery";
    }

    public static DeliveryService Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new DeliveryService();
                return _instance;
            }
        }
    }

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void UpdateParcelStatus(Parcel parcel, string newStatus)
    {
        parcel.UpdateStatus(newStatus);
        NotifyObservers(parcel.TrackingNumber, newStatus);
    }

    private void NotifyObservers(string trackingNumber, string status)
    {
        foreach (var observer in _observers)
        {
            observer.Update(trackingNumber, status);
        }
    }

    public static decimal ProcessPayment(Parcel parcel, IDeliveryStrategy strategy)
    {
        return strategy.CalculateCost(parcel.Weight);
    }
}