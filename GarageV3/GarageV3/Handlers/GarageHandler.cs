using GarageV3.Interfaces;
using GarageV3.Models;

namespace GarageV3.Handlers;

internal sealed class GarageHandler<T> : IHandler<T> where T : Vehicle
{
    private Garage<T>? _garage;
    private Garage<T> CurrentGarage =>
        _garage ?? throw new InvalidOperationException("Skapa ett garage först.");

    public int MaxCapacity => Garage<T>.MaxCapacity;
    public int Capacity => CurrentGarage.Capacity;
    public int Count => CurrentGarage.Count;

    public void CreateGarage(int capacity)
    {
        _garage = new Garage<T>(capacity);
    }

    public IEnumerable<T> GetVehicles()
    {
        return CurrentGarage.GetVehicles();
    }

    public T? FindByRegistration(string registrationNumber)
    {
        return CurrentGarage.FindByRegistration(registrationNumber);
    }

    public bool TryPark(T vehicle, out string errorMessage)
    {
        return CurrentGarage.TryPark(vehicle, out errorMessage);
    }

    public bool Remove(string registrationNumber)
    {
        return CurrentGarage.Remove(registrationNumber);
    }

    public IEnumerable<T> Search(
        string? registrationNumber = null,
        string? color = null,
        int? numberOfWheels = null,
        string? vehicleType = null)
    {
        return CurrentGarage.Search(
            registrationNumber, color, numberOfWheels, vehicleType);
    }

    // räknar fordonen per typ
    public Dictionary<string, int> GetVehicleTypeCounts()
    {
        // sparar typnamnet som nyckel och antalet som värde
        Dictionary<string, int> counts = [];

        foreach (T vehicle in CurrentGarage)
        {
            string type = vehicle.GetType().Name;
            // får noll om typen inte finns än
            counts.TryGetValue(type, out int currentCount);
            counts[type] = currentCount + 1;
        }

        return counts;
    }
}
