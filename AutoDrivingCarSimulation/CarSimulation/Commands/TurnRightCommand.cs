using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.Commands
{
    /// <summary>
    /// Command to rotate the car 90 degrees to the right (clockwise).
    /// </summary>
    public class TurnRightCommand : ICommand
    {
        /// <summary>
        /// Executes the turn right command, updating the car's orientation to the right.
        /// </summary>
        /// <param name="car">The car to turn right.</param>
        public void Execute(Car car)
        {
            car.Orientation = GetNewOrientation(car.Orientation);
        }

        /// <summary>
        /// Gets the new orientation of the car after executing the turn right command.
        /// </summary>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The new orientation of the car after executing the turn right command.</returns>
        public Orientation GetNewOrientation(Orientation currentOrientation)
        {
            return currentOrientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N,
                _ => currentOrientation
            };
        }

        /// <summary>
        /// Gets the new position of the car after executing the turn right command.
        /// The position of the car does not change when turning right.
        /// </summary>
        /// <param name="currentPosition">The current position of the car.</param>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The current position of the car, as the position does not change when turning right.</returns>
        public (int X, int Y) GetNewPosition((int X, int Y) currentPosition, Orientation currentOrientation)
        {
            return currentPosition;
        }
    }
}