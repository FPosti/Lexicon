using GarageV3.Models;

namespace GarageV3.Interfaces;


internal interface IHandler<T> where T : Vehicle
{
    int MaxCapacity { get; }
    int Capacity { get; }
    int Count { get; }

    void CreateGarage(int capacity);
    IEnumerable<T> GetVehicles();
    T? FindByRegistration(string registrationNumber);
    bool TryPark(T vehicle, out string errorMessage);
    bool Remove(string registrationNumber);

    IEnumerable<T> Search(
        string? registrationNumber = null,
        string? color = null,
        int? numberOfWheels = null,
        string? vehicleType = null);

    Dictionary<string, int> GetVehicleTypeCounts();
}
