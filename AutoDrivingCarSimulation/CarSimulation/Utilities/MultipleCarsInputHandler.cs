using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Utilities
{
    /// <summary>
    /// Handles input from the console for scenarios involving multiple cars,
    /// minimizing code repetition by utilizing methods from the base class.
    /// </summary>
    public class MultipleCarsInputHandler : InputHandlerBase
    {
        /// <inheritdoc/>
        public override SimulationInput GetInput()
        {
            // Request field size
            var (width, height) = RequestFieldSize();

            var carInputs = new List<CarInput>();
            var commandsPerCar = new Dictionary<string, List<ICommand>>();

            // Ensure input for at least two cars
            for (int i = 0; i < 2; i++)
            {
                AddCarInput(carInputs, commandsPerCar);
            }

            // Option to add more cars
            while (true)
            {
                DisplayMessage(Constants.AddAnotherCarPrompt);
                if (ReadLine().Trim().ToLower() != "y") break;
                AddCarInput(carInputs, commandsPerCar);
            }

            return new SimulationInput(width, height, carInputs, commandsPerCar);
        }

        /// <summary>
        /// Adds a car's input and commands to the respective collections.
        /// </summary>
        /// <param name="carInputs">Collection of car inputs.</param>
        /// <param name="commandsPerCar">Collection of commands per car.</param>
        private void AddCarInput(List<CarInput> carInputs, Dictionary<string, List<ICommand>> commandsPerCar)
        {
            DisplayMessage(Constants.EnterCarNamePrompt);
            var name = ReadLine();

            var carInput = RequestCarInput(name);
            var commands = RequestCommands(name);

            carInputs.Add(carInput);
            commandsPerCar[name] = commands;
        }
    }
}
