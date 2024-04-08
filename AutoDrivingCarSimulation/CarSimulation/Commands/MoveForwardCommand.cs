using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

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
            car.Position = GetNewPosition(car.Position, car.Orientation);
        }

        /// <summary>
        /// Gets the new position of the car after executing the move forward command.
        /// </summary>
        /// <param name="currentPosition">The current position of the car.</param>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The new position of the car after executing the move forward command.</returns>
        public (int X, int Y) GetNewPosition((int X, int Y) currentPosition, Orientation currentOrientation)
        {
            return currentOrientation switch
            {
                Orientation.N => (currentPosition.X, currentPosition.Y + 1),
                Orientation.E => (currentPosition.X + 1, currentPosition.Y),
                Orientation.S => (currentPosition.X, currentPosition.Y - 1),
                Orientation.W => (currentPosition.X - 1, currentPosition.Y),
                _ => currentPosition
            };
        }

        /// <summary>
        /// Gets the new orientation of the car after executing the move forward command.
        /// The orientation does not change when moving forward.
        /// </summary>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The new orientation of the car after executing the move forward command.</returns>
        public Orientation GetNewOrientation(Orientation currentOrientation)
        {
            return currentOrientation;
        }
    }
}