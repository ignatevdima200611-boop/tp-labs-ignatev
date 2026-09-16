using System;

public class Car
{
    private double _fuel;

    public string Model { get; }
    public double FuelConsumption { get; }
    public double Mileage { get; private set; }

    public Car(string model, double fuelConsumption)
    {
        if (fuelConsumption <= 0)
            throw new ArgumentOutOfRangeException(nameof(fuelConsumption));
        Model = model;
        FuelConsumption = fuelConsumption;
    }

    public void Refuel(double liters)
    {
        if (liters <= 0) throw new ArgumentOutOfRangeException(nameof(liters));
        _fuel += liters;
    }

    public double Drive(double distanceKm)
    {
        double maxDistance = _fuel / FuelConsumption * 100;
        double actual = Math.Min(distanceKm, maxDistance);
        Mileage += actual;
        return actual;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Пример Car ===");
        var car = new Car("Lada Granta", 6.6);
        car.Refuel(20);
        double d = car.Drive(500);
        Console.WriteLine($"{car.Model}: проехали {d:F0} км, пробег {car.Mileage:F0} км");
        Console.WriteLine("\n=== Пример Shape ===");
        ShapeB.ShapeMain();
    }
}