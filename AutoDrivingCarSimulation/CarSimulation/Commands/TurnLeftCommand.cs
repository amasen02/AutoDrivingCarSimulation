using CarSimulation.Enums;
using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Commands
{
    /// <summary>
    /// Command to rotate the car 90 degrees to the left (counterclockwise).
    /// </summary>
    public class TurnLeftCommand : ICommand
    {
        public void Execute(Car car)
        {
            car.Orientation = car.Orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
                _ => car.Orientation
            };
        }
    }
}
