using GarageV3.Interfaces;
namespace GarageV3.Utils;

using GarageV3.Models;

internal sealed class GarageConsole<T> : IUI where T : Vehicle
{
    private readonly IHandler<T> _handler;
    private readonly Func<string, T> _readVehicle;
    private readonly IEnumerable<T> _samples;

    public GarageConsole(
        IHandler<T> handler,
        Func<string, T> readVehicle,
        IEnumerable<T> samples)
    {
        ArgumentNullException.ThrowIfNull(handler);
        ArgumentNullException.ThrowIfNull(readVehicle);
        ArgumentNullException.ThrowIfNull(samples);

        _handler = handler;
        _readVehicle = readVehicle;
        _samples = samples;
    }
    public void Run()
    {
        try
        {
            RunApplication();
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nInmatningen avslutades. Programmet stängs.");
        }
    }
    private void RunApplication()
    {
        Console.WriteLine("Välkommen till garaget");
        int capacity = ConsoleInput.ReadInt(
        $"Ange garagets kapacitet (1-{_handler.MaxCapacity}): ",
        1, _handler.MaxCapacity);
        _handler.CreateGarage(capacity);
        int addSamples = ConsoleInput.ReadInt(
        "Vill du fylla garaget med exempelfordon? 1 = ja, 0 = nej: ", 0, 1);
        if (addSamples == 1)
        {
            VehicleConsole.AddSampleVehicles(_handler, _samples);
        }
        while (true)
        {
            ShowMainMenu(_handler);
            int choice = ConsoleInput.ReadInt("Välj ett alternativ: ", 0, 5); switch (choice)
            {
                case 1:
                    VehicleConsole.DisplayVehicles(_handler.GetVehicles());
                    break;
                case 2:
                    VehicleConsole.ListVehicleTypes(_handler);
                    break;
                case 3:
                    VehicleConsole.ParkVehicle(_handler, _readVehicle);
                    break;
                case 4:
                    VehicleConsole.RemoveVehicle(_handler);
                    break;
                case 5:
                    SearchMenu(_handler);
                    break;
                case 0:
                    Console.WriteLine("Programmet avslutas.");
                    return;
            }
        }
    }
    private static void ShowMainMenu(IHandler<T> handler)
    {
        Console.WriteLine($"\nGarage: {handler.Count}/{handler.Capacity} platser upptagna");
        Console.WriteLine("1. Lista parkerade fordon");
        Console.WriteLine("2. Visa antal fordon per typ");
        Console.WriteLine("3. Parkera ett fordon");
        Console.WriteLine("4. Hämta ut ett fordon");
        Console.WriteLine("5. Sök efter fordon");
        Console.WriteLine("0. Avsluta");
    }
    private static void SearchMenu(IHandler<T> handler)
    {
        while (true)
        {
            Console.WriteLine("\nSökmeny");
            Console.WriteLine("1. Sök via registreringsnummer");
            Console.WriteLine("2. Sök med flera filter");
            Console.WriteLine("0. Tillbaka");
            int choice = ConsoleInput.ReadInt("Välj ett alternativ: ", 0, 2);
            switch (choice)
            {
                case 1:
                    VehicleConsole.FindVehicle(handler);
                    break;
                case 2:
                    VehicleConsole.SearchVehicles(handler);
                    break;
                case 0:
                    return;
            }
        }
    }
}
