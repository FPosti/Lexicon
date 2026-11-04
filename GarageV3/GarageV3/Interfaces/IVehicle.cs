using System;
using System.Collections.Generic;
using System.Text;

namespace GarageV3.Interfaces
{
    public interface IVehicle
    {
        string RegistrationNumber { get; }
        string Color { get; }
        int NumberOfWheels { get; }
    }
}
