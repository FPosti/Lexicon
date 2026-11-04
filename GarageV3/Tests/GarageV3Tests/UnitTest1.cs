using GarageV3;
using GarageV3.Models;

namespace GarageV3Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var car = new Car("ABC123", "Blå", 4, "Bensin");
            Assert.Equal("ABC123", car.RegistrationNumber);
        }

        [Fact]
        public void ParkCar()
        {
            var garage = new Garage<Car>(2);
            var car = new Car("ABC123", "Blå", 4, "Bensin");

            bool result = garage.TryPark(car, out _);

            Assert.True(result);
            Assert.Equal(1, garage.Count);
            Assert.Same(car, garage.FindByRegistration("ABC123"));
        }

        [Fact]
        public void CannotParkWhenFull()
        {
            var garage = new Garage<Car>(1);
            var car1 = new Car("ABC123", "Blå", 4, "Bensin");
            var car2 = new Car("DEF456", "Röd", 4, "Diesel");
            Assert.True(garage.TryPark(car1, out _));

            bool result = garage.TryPark(car2, out string errorMessage);

            Assert.False(result);
            Assert.Equal("Garaget är fullt.", errorMessage);
            Assert.Equal(1, garage.Count);
            Assert.Null(garage.FindByRegistration("DEF456"));
        }
    }
}
