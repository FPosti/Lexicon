namespace GarageLists
{
    internal sealed class Garage
    {
        public const int MaxCapacity = 1000;

        private readonly List<Vehicle> _vehicles;

        public int Capacity { get; }
        public int Count => _vehicles.Count;

        public Garage(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(capacity, MaxCapacity);

            Capacity = capacity;
            _vehicles = new List<Vehicle>(capacity);
        }

        // Search utan sökvillkor ger alla fordon i en separat lista.
        public List<Vehicle> GetVehicles()
        {
            return Search();
        }

        public Vehicle? FindByRegistration(string registrationNumber)
        {
            int index = FindIndexByRegistration(registrationNumber);

            if (index == -1)
            {
                return null;
            }

            return _vehicles[index];
        }

        
        public bool TryPark(Vehicle vehicle, out string errorMessage)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            if (FindByRegistration(vehicle.RegistrationNumber) is not null)
            {
                errorMessage = "Ett fordon med det registreringsnumret är redan parkerat.";
                return false;
            }

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

            // RemoveAt tar bort fordonet på listans index, listan är dynamisk och krymper automatiskt.
            _vehicles.RemoveAt(index);

            return true;
        }

        public List<Vehicle> Search(
            string? registrationNumber = null,
            string? color = null,
            int? numberOfWheels = null,
            string? vehicleType = null)
        {
            // ?. gör att Trim bara körs om värdet inte är null. T
            registrationNumber = registrationNumber?.Trim();
            color = color?.Trim();
            vehicleType = vehicleType?.Trim();

            List<Vehicle> matches = [];

            foreach (Vehicle vehicle in _vehicles)
            {
                if (vehicle is null)
                {
                    continue;
                }

                // OrdinalIgnoreCase gör att till exempel ABC123 och abc123 räknas som lika.
                bool matchesRegistration = string.IsNullOrEmpty(registrationNumber) ||
                    string.Equals(vehicle.RegistrationNumber, registrationNumber,
                        StringComparison.OrdinalIgnoreCase);

                bool matchesColor = string.IsNullOrEmpty(color) ||
                    string.Equals(vehicle.Color, color,
                        StringComparison.OrdinalIgnoreCase);

                bool matchesWheels = numberOfWheels is null ||
                    vehicle.NumberOfWheels == numberOfWheels;

                bool matchesType = string.IsNullOrEmpty(vehicleType) ||
                    string.Equals(vehicle.GetType().Name, vehicleType,
                        StringComparison.OrdinalIgnoreCase);

               
                if (matchesRegistration && matchesColor && matchesWheels && matchesType)
                {
                    matches.Add(vehicle);
                }
            }

            return matches;
        }

        private int FindIndexByRegistration(string registrationNumber)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);
            registrationNumber = registrationNumber.Trim();

            for (int i = 0; i < _vehicles.Count; i++)
            {
                Vehicle? vehicle = _vehicles[i];
                _ = _vehicles[i];

                if (string.Equals(vehicle.RegistrationNumber, registrationNumber,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            // Listans index börjar på 0, så -1 används för att visa att inget fordon hittades.
            return -1;
        }
    }
}
