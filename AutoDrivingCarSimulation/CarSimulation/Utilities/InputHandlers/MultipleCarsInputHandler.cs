using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.Utilities.InputHandlers
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
            var (width, height) = RequestFieldSize();
            var carInputs = new List<CarInput>();
            var commandsPerCar = new Dictionary<string, List<ICommand>>();

            PopulateCarInputsAndCommands(carInputs, commandsPerCar);

            return new SimulationInput(width, height, carInputs, commandsPerCar);
        }

        /// <summary>
        /// Populates car inputs and commands.
        /// </summary>
        /// <param name="carInputs">Collection of car inputs.</param>
        /// <param name="commandsPerCar">Collection of commands per car.</param>
        private void PopulateCarInputsAndCommands(List<CarInput> carInputs, Dictionary<string, List<ICommand>> commandsPerCar)
        {
            do
            {
                var (name, carInput, commands) = RequestCarInputAndCommands();
                AddCarInputAndCommands(carInputs, commandsPerCar, name, carInput, commands);
            } while (PromptToAddAnotherCar());
        }

        /// <summary>
        /// Requests car input and commands.
        /// </summary>
        /// <returns>A tuple containing car name, car input, and commands.</returns>
        private (string Name, CarInput CarInput, List<ICommand> Commands) RequestCarInputAndCommands()
        {
            DisplayMessage(MessageConstants.EnterCarNamePrompt);
            var name = ReadLine();
            var carInput = RequestCarInput(name);
            var commands = RequestCommands(name);
            return (name, carInput, commands);
        }

        /// <summary>
        /// Adds car input and commands to the respective collections.
        /// </summary>
        /// <param name="carInputs">Collection of car inputs.</param>
        /// <param name="commandsPerCar">Collection of commands per car.</param>
        /// <param name="name">Name of the car.</param>
        /// <param name="carInput">Car input data.</param>
        /// <param name="commands">Commands for the car.</param>
        private void AddCarInputAndCommands(List<CarInput> carInputs, Dictionary<string, List<ICommand>> commandsPerCar, string name, CarInput carInput, List<ICommand> commands)
        {
            carInputs.Add(carInput);
            commandsPerCar[name] = commands;
        }

        /// <summary>
        /// Prompts the user to add another car.
        /// </summary>
        /// <returns>True if the user wants to add another car, otherwise false.</returns>
        private bool PromptToAddAnotherCar()
        {
            DisplayMessage(MessageConstants.AddAnotherCarPrompt);
            return ReadLine().Trim().ToLower() == "y";
        }
    }
}
