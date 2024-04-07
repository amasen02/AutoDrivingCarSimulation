using CarSimulation.Enums;
using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Commands
{
    /// <summary>
    /// Command to move the car forward one unit in its current orientation.
    /// </summary>
    public class MoveForwardCommand : ICommand
    {
        /// <summary>
        /// Executes the move forward command, adjusting the car's position based on its orientation.
        /// </summary>
        /// <param name="car">The car to move forward.</param>
        public void Execute(Car car)
        {
            switch (car.Orientation)
            {
                case Orientation.N:
                    car.Position = (car.Position.X, car.Position.Y + 1);
                    break;
                case Orientation.E:
                    car.Position = (car.Position.X + 1, car.Position.Y);
                    break;
                case Orientation.S:
                    car.Position = (car.Position.X, car.Position.Y - 1);
                    break;
                case Orientation.W:
                    car.Position = (car.Position.X - 1, car.Position.Y);
                    break;
            }
        }
    }
}
