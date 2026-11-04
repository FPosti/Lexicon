using GarageV3.Handlers;
using GarageV3.Interfaces;
using GarageV3.Models;
using GarageV3.Utils;
using System.Text;


Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

IHandler<Vehicle> handler = new GarageHandler<Vehicle>();

GarageConsole<Vehicle> garageConsole = new(
    handler,
    VehicleConsole.ReadVehicle,
    VehicleConsole.CreateSamples());

garageConsole.Run();
