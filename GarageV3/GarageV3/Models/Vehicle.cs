using GarageV3.Interfaces;
using GarageV3.Validation;
namespace GarageV3.Models
{
    // abstract gör att man måste skapa en underklass som Car
    public abstract class Vehicle : IVehicle
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

        // varje fordonstyp lägger till sin egen info
        protected abstract string GetSpecificDetails();

        // WriteLine använder den här texten när vi skriver ut fordonet
        public sealed override string ToString()
        {
            return $"{VehicleTypeName}: {RegistrationNumber}, färg: {Color}, " +
                $"antal hjul: {NumberOfWheels}, {GetSpecificDetails()}";
        }
    }
}
