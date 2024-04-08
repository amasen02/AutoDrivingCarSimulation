using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Utilities.InputHandlers
{
    /// <summary>
    /// Handles input from the console for a single car simulation scenario,
    /// leveraging common input functionalities from the base class.
    /// </summary>
    public class SingleCarInputHandler : InputHandlerBase
    {
        public override SimulationInput GetInput()
        {
            var (width, height) = RequestFieldSize();
            var carInput = RequestCarInput();
            var commands = RequestCommands();
            var commandsPerCar = CreateCommandsPerCarDictionary(carInput, commands);

            return new SimulationInput(width, height, new List<CarInput> { carInput }, commandsPerCar);
        }

        private Dictionary<string, List<ICommand>> CreateCommandsPerCarDictionary(CarInput carInput, List<ICommand> commands)
        {
            return new Dictionary<string, List<ICommand>> { { carInput.Name, commands } };
        }
    }
}
