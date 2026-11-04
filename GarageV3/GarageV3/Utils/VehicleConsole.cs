using GarageV3.Interfaces;
using GarageV3.Models;

namespace GarageV3.Utils;

internal static class VehicleConsole
{
    private static readonly List<string> VehicleTypeNames =
    [
    nameof(Motorcycle), nameof(Airplane), nameof(Car), nameof(Bus), nameof(Boat)
    ];

    public static void DisplayVehicles<T>(IEnumerable<T> vehicles)
    where T : Vehicle
    {
        // håller koll på om det fanns något att skriva ut
        bool foundAny = false;

        foreach (T vehicle in vehicles)
        {
            foundAny = true;
            Console.WriteLine(vehicle);
        }

        if (!foundAny)
        {
            Console.WriteLine("Inga fordon att visa.");
        }
    }

    public static void ListVehicleTypes<T>(IHandler<T> garage)
    where T : Vehicle
    {
        if (garage.Count == 0)
        {
            Console.WriteLine("Garaget är tomt.");
            return;
        }
        Dictionary<string, int> counts = garage.GetVehicleTypeCounts();
        foreach (string type in VehicleTypeNames)
        {
            if (counts.TryGetValue(type, out int count))
            {
                Console.WriteLine($"{GetVehicleTypeDisplayName(type)}: {count}");
            }
        }
    }

    public static void ParkVehicle<T>(
        IHandler<T> garage,
        Func<string, T> readVehicle)
        where T : Vehicle
    {
        // kollar plats först så man slipper fylla i allt i onödan
        if (garage.Count >= garage.Capacity)
        {
            Console.WriteLine("Garaget är fullt.");
            return;
        }

        string registration =
            ConsoleInput.ReadRegistrationNumber("Registreringsnummer: ");

        // samma regnummer får inte finnas två gånger
        if (garage.FindByRegistration(registration) is not null)
        {
            Console.WriteLine(
                "Ett fordon med det registreringsnumret är redan parkerat.");
            return;
        }

        // kör funktionen som frågar efter resten och skapar fordonet
        T vehicle = readVehicle(registration);

        if (garage.TryPark(vehicle, out string errorMessage))
        {
            Console.WriteLine(
                $"Fordonet {vehicle.RegistrationNumber} har parkerats.");
        }
        else
        {
            Console.WriteLine(errorMessage);
        }
    }

    public static void RemoveVehicle<T>(IHandler<T> garage)
    where T : Vehicle
    {
        string registration = ConsoleInput.ReadRegistrationNumber(
            "Ange registreringsnummer att hämta ut: ");

        Console.WriteLine(garage.Remove(registration)
            ? "Fordonet har hämtats ut."
            : "Inget fordon med det registreringsnumret hittades.");
    }

    public static void FindVehicle<T>(IHandler<T> garage)
    where T : Vehicle
    {
        string registration = ConsoleInput.ReadRegistrationNumber("Registreringsnummer att söka efter: ");
        T? vehicle = garage.FindByRegistration(registration);

        if (vehicle is null)
        {
            Console.WriteLine("Inget fordon med det registreringsnumret hittades.");
        }
        else
        {
            Console.WriteLine(vehicle);
        }
    }

    public static void SearchVehicles<T>(IHandler<T> garage)
    where T : Vehicle
    {
        Console.WriteLine("Lämna fält tomma för valfria värden. Alla angivna sökvillkor måste stämma.");
        string? registration = ConsoleInput.ReadOptionalRegistrationNumber("Registreringsnummer (lämna tomt för alla): ");
        string? color = ConsoleInput.ReadOptionalText("Färg (lämna tomt för alla): ");
        int? wheels = ConsoleInput.ReadOptionalWheelCount();
        string? type = ReadVehicleType(allowAny: true);

        DisplayVehicles(garage.Search(registration, color, wheels, type));
    }

    public static void AddSampleVehicles<T>(
    IHandler<T> garage,
    IEnumerable<T> samples)
    where T : Vehicle
    {
        int added = 0;
        int total = 0;

        foreach (T vehicle in samples)
        {
            total++;

            // out _ gör att vi hoppar över feltexten här
            if (garage.TryPark(vehicle, out _))
            {
                added++;
            }
        }

        Console.WriteLine(
            $"{added} exempelfordon parkerades. " +
            $"{total - added} kunde inte parkeras.");
    }

    // visar svenska namn i menyn
    private static string GetVehicleTypeDisplayName(string type)
    {
        return type switch
        {
            nameof(Motorcycle) => "Motorcykel",
            nameof(Airplane) => "Flygplan",
            nameof(Car) => "Bil",
            nameof(Bus) => "Buss",
            nameof(Boat) => "Båt",
            _ => type,
        };
    }

    private static string? ReadVehicleType(bool allowAny)
    {
        for (int i = 0; i < VehicleTypeNames.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {GetVehicleTypeDisplayName(VehicleTypeNames[i])}");
        }

        if (allowAny)
        {
            Console.WriteLine("0. Alla fordonstyper");
        }

        int choice = ConsoleInput.ReadInt("Välj fordonstyp: ", allowAny ? 0 : 1, VehicleTypeNames.Count);
        // listan börjar på noll så menyvalet behöver minskas med ett
        return choice == 0 ? null : VehicleTypeNames[choice - 1];
    }

    // här skapas rätt sorts fordon efter menyvalet
    public static Vehicle ReadVehicle(string registration)
    {
        string? type = ReadVehicleType(allowAny: false);
        string color = ConsoleInput.ReadRequiredText("Färg: ");
        int wheels = ConsoleInput.ReadInt("Antal hjul: ", 0);

        Vehicle vehicle;

        switch (type)
        {
            case nameof(Motorcycle):
                int cylinderVolumeCm3 =
                    ConsoleInput.ReadInt("Cylindervolym (cm3): ", 1);

                vehicle = new Motorcycle(
                    registration, color, wheels, cylinderVolumeCm3);
                break;

            case nameof(Airplane):
                int engines = ConsoleInput.ReadInt("Antal motorer: ", 1);

                vehicle = new Airplane(
                    registration, color, wheels, engines);
                break;

            case nameof(Car):
                string fuelType =
                    ConsoleInput.ReadRequiredText("Bränsletyp: ");

                vehicle = new Car(
                    registration, color, wheels, fuelType);
                break;

            case nameof(Bus):
                int seats = ConsoleInput.ReadInt("Antal sittplatser: ", 1);

                vehicle = new Bus(
                    registration, color, wheels, seats);
                break;

            case nameof(Boat):
                decimal length =
                    ConsoleInput.ReadPositiveDecimal("Längd i meter: ");

                vehicle = new Boat(
                    registration, color, wheels, length);
                break;

            default:
                throw new InvalidOperationException("Okänd fordonstyp.");
        }

        return vehicle;
    }

    // några fordon att testa garaget med
    public static IEnumerable<Vehicle> CreateSamples()
    {
        return
        [
            new Car("ABC123", "svart", 4, "Bensin"),
        new Motorcycle("MCT12A", "rosa", 3, 600),
        new Airplane("FLY123", "vit", 3, 2),
        new Bus("BUS123", "röd", 6, 40),
        new Boat("BAT123", "blå", 0, 5.5m)
        ];
    }
}
