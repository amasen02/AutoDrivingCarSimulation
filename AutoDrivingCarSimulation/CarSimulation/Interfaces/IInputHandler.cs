using CarSimulation.Models;

namespace CarSimulation.Interfaces
{
    /// <summary>
    /// Interface for handling user input.
    /// </summary>
    public interface IInputHandler
    {
        /// <summary>
        /// Gets the simulation input from the user.
        /// </summary>
        /// <returns>The simulation input.</returns>
        SimulationInput GetInput();
    }
}
