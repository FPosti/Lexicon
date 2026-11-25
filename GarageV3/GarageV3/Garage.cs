using GarageV3.Models;
using System.Collections;
namespace GarageV3;

// where gör att T måste vara Vehicle eller ärva från Vehicle
public sealed class Garage<T> : IEnumerable<T> where T : Vehicle
{
    public const int MaxCapacity = 1000;
    // readonly låser själva listan men innehållet kan ändras
    private readonly List<T> _vehicles;
    public int Capacity { get; }
    public int Count => _vehicles.Count;
    public Garage(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(capacity, MaxCapacity);
        Capacity = capacity;
        _vehicles = new List<T>(capacity);
    }
    public List<T> GetVehicles()
    {
        return Search();
    }
    public T? FindByRegistration(string registrationNumber)
    {
        int index = FindIndexByRegistration(registrationNumber); 
        if (index == -1)
        {
            return null;
        }
        return _vehicles[index];
    }

    public bool TryPark(T vehicle, out string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        if (FindByRegistration(vehicle.RegistrationNumber) is not null)
        {
            errorMessage = "Ett fordon med det registreringsnumret är redan parkerat.";
            return false;
        }
        // listan kan växa så vi måste kolla max antal själva
        if (Count >= Capacity)
        {
            errorMessage = "Garaget är fullt.";
            return false;
        }
        _vehicles.Add(vehicle);
        errorMessage = string.Empty;
        return true;
    }
    public bool Remove(string registrationNumber)
    {
        int index = FindIndexByRegistration(registrationNumber);
        if (index == -1)
        {
            return false;
        }
        _vehicles.RemoveAt(index);
        return true;
    }

    public List<T> Search(
    string? registrationNumber = null,
    string? color = null,
    int? numberOfWheels = null,
    string? vehicleType = null)
    {
        // ?. gör att Trim bara körs om texten inte är null
        registrationNumber = registrationNumber?.Trim();
        color = color?.Trim();
        vehicleType = vehicleType?.Trim();
        // alla ifyllda filter måste stämma
        // Where väljer ut träffarna och .. samlar dom i en ny lista
        return [.. _vehicles
        .Where(vehicle =>
        (string.IsNullOrWhiteSpace(registrationNumber) ||
        vehicle.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase)) &&
        (string.IsNullOrWhiteSpace(color) ||
        vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&
        (!numberOfWheels.HasValue ||
        vehicle.NumberOfWheels == numberOfWheels.Value) &&
        (string.IsNullOrWhiteSpace(vehicleType) ||
        vehicle.GetType().Name.Equals(
        vehicleType, StringComparison.OrdinalIgnoreCase)))];
    }
    // använder listans iterator så foreach funkar på garaget
    public IEnumerator<T> GetEnumerator()
    {
        return _vehicles.GetEnumerator();
    }
    // varianten utan T använder samma iterator
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private int FindIndexByRegistration(string registrationNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);
        registrationNumber = registrationNumber.Trim();
        for (int i = 0; i < _vehicles.Count; i++)
        {
            T vehicle = _vehicles[i];
            // stora och små bokstäver ska ge samma träff
            if (string.Equals(vehicle.RegistrationNumber, registrationNumber,
            StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }
        // hittade inget nummer i listan
        return -1;
    }
}
