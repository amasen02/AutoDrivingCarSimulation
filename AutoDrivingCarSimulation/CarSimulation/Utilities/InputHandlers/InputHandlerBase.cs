using CarSimulation.Commands;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;
using ICommand = CarSimulation.Interfaces.ICommand;

namespace CarSimulation.Utilities.InputHandlers
{
    /// <summary>
    /// Provides base functionality for handling user input, including methods for parsing simulation field dimensions,
    /// car details, and commands. This class is intended to be extended by specific input handlers for different simulation scenarios.
    /// </summary>
    public abstract class InputHandlerBase : IInputHandler
    {
        /// <summary>
        /// Retrieves simulation input data. Implementing classes must gather and return all necessary data for the simulation scenario.
        /// </summary>
        /// <returns>Simulation input data populated based on user input.</returns>
        public abstract SimulationInput GetInput();

        /// <summary>
        /// Displays a message to the console for user interaction.
        /// </summary>
        /// <param name="message">The message to be displayed to the user.</param>
        protected void DisplayMessage(string message) => Console.WriteLine(message);

        /// <summary>
        /// Reads a line of input from the console, provided by the user.
        /// </summary>
        /// <returns>The user-provided input as a string.</returns>
        protected string ReadLine() => Console.ReadLine();

        /// <summary>
        /// Requests and parses the width and height of the simulation field from user input.
        /// </summary>
        /// <returns>A tuple containing the parsed width and height of the field.</returns>
        protected (int Width, int Height) RequestFieldSize()
        {
            while (true)
            {
                try
                {
                    DisplayMessage(MessageConstants.FieldSizePrompt);
                    return ParseWidthHeight(ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format(MessageConstants.GeneralInputFormatError, ex.Message));
                }
            }
        }

        /// <summary>
        /// Requests and parses the starting position and orientation of a car from user input.
        /// </summary>
        /// <param name="carName">The name of the car for which the details are being requested. Used for personalized prompts in multi-car scenarios.</param>
        /// <returns>A CarInput object containing the parsed position and orientation of the car.</returns>
        protected CarInput RequestCarInput(string carName = "")
        {
            while (true)
            {
                try
                {
                    string prompt = string.IsNullOrEmpty(carName)
                        ? MessageConstants.CarPositionPrompt
                        : string.Format(MessageConstants.CarPositionPromptWithName, carName);
                    DisplayMessage(prompt);
                    return ParseCarDetails(ReadLine(), carName);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(string.Format(MessageConstants.GeneralInputFormatError, ex.Message));
                }
            }
        }

        /// <summary>
        /// Requests and parses a sequence of commands for moving and turning the car.
        /// </summary>
        /// <param name="carName">The name of the car for which the commands are being requested. Used for personalized prompts in multi-car scenarios.</param>
        /// <returns>A list of ICommand objects representing the sequence of commands for the car.</returns>
        protected List<ICommand> RequestCommands(string carName = "")
        {
            while (true)
            {
                try
                {
                    string prompt = string.IsNullOrEmpty(carName)
                        ? MessageConstants.CommandsPrompt
                        : string.Format(MessageConstants.CommandsPromptWithName, carName);
                    DisplayMessage(prompt);
                    return ParseCommands(ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(string.Format(MessageConstants.GeneralInputFormatError, ex.Message));
                }
            }
        }

        /// <summary>
        /// Parses a string containing the width and height of the simulation field.
        /// </summary>
        /// <param name="widthHeightLine">The input string containing the width and height separated by a space.</param>
        /// <returns>A tuple containing the parsed width and height as integers.</returns>
        /// <exception cref="FormatException">Thrown when the input format does not match the expected 'width height' format.</exception>
        private (int Width, int Height) ParseWidthHeight(string widthHeightLine)
        {
            var parts = widthHeightLine.Split(' ');
            ValidateWidthHeightInput(parts);
            return CreateWidthHeight(parts);
        }

        /// <summary>
        /// Validates the parts of the input string representing width and height.
        /// </summary>
        /// <param name="parts">The parts of the input string to be validated.</param>
        /// <exception cref="FormatException">Thrown when the input format is invalid.</exception>
        private void ValidateWidthHeightInput(string[] parts)
        {
            if (parts.Length != 2 || !int.TryParse(parts[0], out _) || !int.TryParse(parts[1], out _))
            {
                throw new FormatException(MessageConstants.InvalidFieldSizeFormat);
            }
        }

        /// <summary>
        /// Creates a tuple containing width and height from the parsed parts.
        /// </summary>
        /// <param name="parts">The parsed parts of the input string.</param>
        /// <returns>A tuple containing the parsed width and height as integers.</returns>
        private (int Width, int Height) CreateWidthHeight(string[] parts)
        {
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }

        /// <summary>
        /// Parses a string containing the starting position (X and Y coordinates) and orientation of a car.
        /// </summary>
        /// <param name="carDetailsLine">The input string containing the X coordinate, Y coordinate, and orientation, separated by spaces.</param>
        /// <param name="carName">The name of the car.</param>
        /// <returns>A CarInput object containing the parsed details of the car.</returns>
        /// <exception cref="FormatException">Thrown when the input format does not match the expected 'X Y Orientation' format.</exception>
        private CarInput ParseCarDetails(string carDetailsLine, string carName)
        {
            var parts = carDetailsLine.Split(' ');
            ValidateInput(parts);
            return CreateCarInput(parts, carName);
        }

        /// <summary>
        /// Validates the parts of the input string.
        /// </summary>
        /// <param name="parts">The parts of the input string to be validated.</param>
        /// <exception cref="FormatException">Thrown when the input format is invalid.</exception>
        private void ValidateInput(string[] parts)
        {
            if (parts.Length != 3 || !int.TryParse(parts[0], out _) || !int.TryParse(parts[1], out _) ||
                !Enum.TryParse(parts[2], true, out Orientation _))
            {
                throw new FormatException(MessageConstants.InvalidCarDetailsFormat);
            }
        }

        /// <summary>
        /// Creates a CarInput object from the parsed parts.
        /// </summary>
        /// <param name="parts">The parsed parts of the input string.</param>
        /// <param name="carName">The name of the car.</param>
        /// <returns>A CarInput object containing the parsed details of the car.</returns>
        private CarInput CreateCarInput(string[] parts, string carName)
        {
            return new CarInput
            {
                X = int.Parse(parts[0]),
                Y = int.Parse(parts[1]),
                Orientation = Enum.Parse<Orientation>(parts[2], true),
                Name = carName ?? MessageConstants.DefaultCarName
            };
        }

        /// <summary>
        /// Parses a string of characters into a list of commands for controlling a car.
        /// </summary>
        /// <param name="commandsLine">The input string where each character represents a command ('L' for left, 'R' for right, 'F' for forward).</param>
        /// <returns>A list of ICommand objects corresponding to the parsed commands.</returns>
        /// <exception cref="FormatException">Thrown when the input string contains invalid commands.</exception>
        private List<ICommand> ParseCommands(string commandsLine)
        {
            var commands = new List<ICommand>();
            foreach (var commandChar in commandsLine.ToUpper())
            {
                ICommand command = commandChar switch
                {
                    'L' => new TurnLeftCommand(),
                    'R' => new TurnRightCommand(),
                    'F' => new MoveForwardCommand(),
                    _ => throw new FormatException(string.Format(MessageConstants.InvalidCommandFormat, commandChar))
                };
                commands.Add(command);
            }
            return commands;
        }
    }
}