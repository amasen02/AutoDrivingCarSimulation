using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.Interfaces
{
    /// <summary>
    /// Defines the required functionality for a car within the auto-driving car simulation.
    /// </summary>
    public interface ICar
    {
        /// <summary>
        /// The name of the car.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The current position of the car represented as X and Y coordinates.
        /// </summary>
        (int X, int Y) Position { get; set; }

        /// <summary>
        /// The current orientation of the car.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Applies a command to the car, updating its position or orientation.
        /// </summary>
        /// <param name="command">The command to apply.</param>
        /// <param name="field">The simulation field to consider for boundary checks.</param>
        void ApplyCommand(ICommand command, Field field);
    }
}
