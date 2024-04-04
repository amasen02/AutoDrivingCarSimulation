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
    }
}
