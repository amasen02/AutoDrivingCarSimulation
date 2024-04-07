using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Utilities
{
    /// <summary>
    /// Handles input from the console for a single car simulation scenario,
    /// leveraging common input functionalities from the base class.
    /// </summary>
    public class SingleCarInputHandler : InputHandlerBase
    {
        /// <inheritdoc/>
        public override SimulationInput GetInput()
        {
            // Request field size
            var (width, height) = RequestFieldSize();

            // Request car input without specifying a name
            var carInput = RequestCarInput();

            // Request commands for the car
            var commands = RequestCommands();

            // Prepare and return simulation input
            var commandsPerCar = new Dictionary<string, List<ICommand>> { { carInput.Name, commands } };
            return new SimulationInput(width, height, new List<CarInput> { carInput }, commandsPerCar);
        }
    }
}
