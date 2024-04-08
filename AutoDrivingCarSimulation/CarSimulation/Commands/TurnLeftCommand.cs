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
        /// <summary>
        /// Executes the turn left command, updating the car's orientation to the left.
        /// </summary>
        /// <param name="car">The car to turn left.</param>
        public void Execute(Car car)
        {
            car.Orientation = GetNewOrientation(car.Orientation);
        }

        /// <summary>
        /// Gets the new orientation of the car after executing the turn left command.
        /// </summary>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The new orientation of the car after executing the turn left command.</returns>
        public Orientation GetNewOrientation(Orientation currentOrientation)
        {
            return currentOrientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
                _ => currentOrientation
            };
        }

        /// <summary>
        /// Gets the new position of the car after executing the turn left command.
        /// The position of the car does not change when turning left.
        /// </summary>
        /// <param name="currentPosition">The current position of the car.</param>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The current position of the car, as the position does not change when turning left.</returns>
        public (int X, int Y) GetNewPosition((int X, int Y) currentPosition, Orientation currentOrientation)
        {
            return currentPosition;
        }
    }
}