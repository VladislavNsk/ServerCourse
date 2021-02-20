using Microsoft.Extensions.Logging;

namespace SimpleLog
{
    public class Car
    {
        public string Model { get; set; }

        public double MaxSpeed { get; set; }

        public double Price { get; set; }

        public double CurrentSpeed { get; set; }

        private readonly ILogger<Car> _logger;

        public Car(double price, double maxSpeed, string model, ILogger<Car> logger)
        {
            Price = price;
            MaxSpeed = maxSpeed;
            Model = model;
            _logger = logger;
        }

        public void Go()
        {
            CurrentSpeed += 10;
            _logger.LogDebug(20, $"Текущая скорость: {CurrentSpeed}");
        }
    }
}
