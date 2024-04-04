using CarSimulation.Enums;
using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Commands
{
    /// <summary>
    /// Command to rotate the car 90 degrees to the right (clockwise).
    /// </summary>
    public class TurnRightCommand : ICommand
    {
        public void Execute(Car car)
        {
            car.Orientation = car.Orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N,
                _ => car.Orientation
            };
        }
    }
}
