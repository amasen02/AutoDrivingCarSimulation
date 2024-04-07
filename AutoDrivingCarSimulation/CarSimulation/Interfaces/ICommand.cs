using CarSimulation.Enums;
using CarSimulation.Models;

namespace CarSimulation.Interfaces
{
    /// <summary>
    /// Defines the interface for command objects that can be executed on a car.
    /// Commands encapsulate actions such as moving forward or turning, altering the state of the car.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command on the specified car, modifying its state according to the command's action.
        /// </summary>
        /// <param name="car">The car on which to execute the command.</param>
        void Execute(Car car);

        /// <summary>
        /// Gets the new position of the car after executing the command.
        /// </summary>
        /// <param name="currentPosition">The current position of the car.</param>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The new position of the car after executing the command.</returns>
        (int X, int Y) GetNewPosition((int X, int Y) currentPosition, Orientation currentOrientation);

        /// <summary>
        /// Gets the new orientation of the car after executing the command.
        /// </summary>
        /// <param name="currentOrientation">The current orientation of the car.</param>
        /// <returns>The new orientation of the car after executing the command.</returns>
        Orientation GetNewOrientation(Orientation currentOrientation);
    }
}