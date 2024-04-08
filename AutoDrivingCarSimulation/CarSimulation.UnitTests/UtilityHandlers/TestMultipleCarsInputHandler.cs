using CarSimulation.Commands;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.UnitTests.UtilityHandlers
{
    /// <summary>
    /// A test-specific input handler designed for providing predetermined simulation inputs for scenarios involving multiple cars.
    /// It allows dynamic configuration of car inputs and command sequences, facilitating a variety of testing scenarios.
    /// </summary>
    public class TestMultipleCarsInputHandler : IInputHandler
    {
        /// <summary>
        /// Gets or sets the overrides for the initial settings of cars in the simulation. This includes their positions and orientations.
        /// </summary>
        public List<CarInput> CarInputsOverride { get; set; }

        /// <summary>
        /// Gets or sets the overrides for the command sequences to be executed for each car.
        /// Each car's name is mapped to its list of commands.
        /// </summary>
        public Dictionary<string, List<ICommand>> CommandsOverride { get; set; }

        /// <summary>
        /// Constructs a new instance of the TestMultipleCarsInputHandler class with optional overrides for car inputs and commands.
        /// </summary>
        /// <param name="carInputsOverride">Optional parameter for overriding the initial settings of cars in the simulation.</param>
        /// <param name="commandsOverride">Optional parameter for overriding the command sequences for each car.</param>
        public TestMultipleCarsInputHandler(List<CarInput>? carInputsOverride = null, Dictionary<string, List<ICommand>>? commandsOverride = null)
        {
            CarInputsOverride = carInputsOverride ?? new List<CarInput>();
            CommandsOverride = commandsOverride ?? new Dictionary<string, List<ICommand>>();
        }

        /// <summary>
        /// Retrieves the simulation inputs, using provided overrides for car inputs and commands if available.
        /// Falls back to default values if no overrides are specified.
        /// </summary>
        /// <returns>A populated SimulationInput object based on specified or default configuration.</returns>
        public SimulationInput GetInput()
        {
            var carInputs = CarInputsOverride.Any() ? CarInputsOverride : GetDefaultCarInputs();
            var commandsPerCar = CommandsOverride.Any() ? CommandsOverride : GetDefaultCommandsPerCar();

            int fieldWidth = 10;
            int fieldHeight = 10;

            return new SimulationInput(fieldWidth, fieldHeight, carInputs, commandsPerCar);
        }

        /// <summary>
        /// Provides default car inputs for testing in case specific overrides are not supplied.
        /// This method can be customized to include a range of default testing scenarios.
        /// </summary>
        /// <returns>A list of CarInput objects to be used as default simulation inputs.</returns>
        private List<CarInput> GetDefaultCarInputs()
        {
            return new List<CarInput>
            {
                new CarInput { Name = "Car1", X = 0, Y = 0, Orientation = Orientation.N },
                // Additional default cars can be specified here for broader test coverage.
            };
        }

        /// <summary>
        /// Supplies a default set of commands for each car in case specific overrides are not provided.
        /// This facilitates basic movement and orientation tests.
        /// </summary>
        /// <returns>A dictionary mapping each car's name to its list of default ICommand objects.</returns>
        private Dictionary<string, List<ICommand>> GetDefaultCommandsPerCar()
        {
            return new Dictionary<string, List<ICommand>>
            {
                {
                    "Car1", new List<ICommand>
                    {
                        new MoveForwardCommand(), new MoveForwardCommand(),
                        new TurnRightCommand(), new MoveForwardCommand()
                    }
                },
            };
        }

    }
}
