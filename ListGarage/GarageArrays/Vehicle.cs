namespace GarageArrays
{
    // En abstrakt klass samlar det som fordonstyperna har gemensamt.
    // Den kan inte skapas med new Vehicle; i stället skapar man exempelvis en Car.
    internal abstract class Vehicle
    {
        public string RegistrationNumber { get; }
        public string Color { get; }
        public int NumberOfWheels { get; }

        protected abstract string VehicleTypeName { get; }

        protected Vehicle(string registrationNumber, string color, int numberOfWheels)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(color);
            ArgumentOutOfRangeException.ThrowIfNegative(numberOfWheels);

            if (!RegistrationNumberValidator.TryParse(registrationNumber, out string registration))
            {
                throw new ArgumentException(RegistrationNumberValidator.ErrorMessage, nameof(registrationNumber));
            }

            RegistrationNumber = registration;
            Color = color.Trim();
            NumberOfWheels = numberOfWheels;
        }

        // abstract betyder att varje underklass måste skriva sin egen version av metoden.
        protected abstract string GetSpecificDetails();

        // ToString bestämmer texten som visas när exempelvis Console.WriteLine skriver ut ett fordon.
        public sealed override string ToString()
        {
            return $"{VehicleTypeName}: {RegistrationNumber}, färg: {Color}, " +
                $"antal hjul: {NumberOfWheels}, {GetSpecificDetails()}";
        }
    }
}
