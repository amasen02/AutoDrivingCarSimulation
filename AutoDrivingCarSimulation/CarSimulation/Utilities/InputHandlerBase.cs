using System;
using System.Collections.Generic;
using CarSimulation.Commands;
using CarSimulation.Enums;
using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Utilities
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
        /// <example>Format: 'width height' (e.g., '10 20')</example>
        protected (int Width, int Height) RequestFieldSize()
        {
            DisplayMessage("Please enter the width and height of the simulation field, separated by a space (e.g., '10 20'):");
            return ParseWidthHeight(ReadLine());
        }

        /// <summary>
        /// Requests and parses the starting position and orientation of a car from user input.
        /// </summary>
        /// <param name="carName">The name of the car for which the details are being requested. Used for personalized prompts in multi-car scenarios.</param>
        /// <returns>A CarInput object containing the parsed position and orientation of the car.</returns>
        /// <example>Format: 'X Y Orientation' (e.g., '1 2 N')</example>
        protected CarInput RequestCarInput(string carName = "")
        {
            while (true)
            {
                try
                {
                    string prompt = string.IsNullOrEmpty(carName) ?
                         "Please enter the starting position (X Y) and orientation (N, E, S, W) of the car:" :
                         $"Please enter the position (X Y) and facing direction (N, E, S, W) for the car named {carName}:";
                    DisplayMessage($"{prompt} (e.g., '1 2 N'):");
                    // Destructure the tuple returned by ParseCarDetails
                    var (x, y, orientation) = ParseCarDetails(ReadLine());

                    // Use the destructured values to instantiate a CarInput object
                    return new CarInput
                    {
                        X = x,
                        Y = y,
                        Orientation = orientation,
                        Name = carName ?? "DefaultCar"
                    };
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} Please try again.");
                }
            }
        }

        /// <summary>
        /// Requests and parses a sequence of commands for moving and turning the car.
        /// </summary>
        /// <param name="carName">The name of the car for which the commands are being requested. Used for personalized prompts in multi-car scenarios.</param>
        /// <returns>A list of ICommand objects representing the sequence of commands for the car.</returns>
        /// <example>Format: A string of characters where 'L' = turn left, 'R' = turn right, 'F' = move forward (e.g., 'LFFR')</example>
        protected List<ICommand> RequestCommands(string carName = "")
        {
         while (true)
            {
                try
                {
                    string prompt = string.IsNullOrEmpty(carName) ? 
                        "Please enter the sequence of commands for the car:" : 
                        $"Please enter the sequence of commands for {carName} car:";
                    DisplayMessage($"{prompt} (e.g., 'LFFR') where 'L' = turn left, 'R' = turn right, 'F' = move forward:");
                    return ParseCommands(ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message} Please try again.");
                }
            }
        }

        /// <summary>
        /// Parses a string containing width and height values, ensuring they are in the correct format.
        /// </summary>
        /// <param name="widthHeightLine">The input string containing the width and height separated by a space.</param>
        /// <returns>A tuple containing the parsed width and height as integers.</returns>
        /// <exception cref="FormatException">Thrown when the input format does not match the expected 'width height' format.</exception>
        private (int Width, int Height) ParseWidthHeight(string widthHeightLine)
        {
            var parts = widthHeightLine.Split(' ');
            if (parts.Length != 2 || !int.TryParse(parts[0], out int width) || !int.TryParse(parts[1], out int height))
                throw new FormatException("Invalid format. Please enter width and height as two integers separated by a space (e.g., '10 20').");
            return (width, height);
        }

        /// <summary>
        /// Parses a string containing the starting position (X and Y coordinates) and orientation of a car.
        /// </summary>
        /// <param name="carDetailsLine">The input string containing the X coordinate, Y coordinate, and orientation, separated by spaces.</param>
        /// <returns>A tuple containing the parsed X and Y coordinates as integers, and the orientation as an <see cref="Orientation"/>.</returns>
        /// <exception cref="FormatException">Thrown when the input format does not match the expected 'X Y Orientation' format.</exception>
        private (int X, int Y, Orientation Orientation) ParseCarDetails(string carDetailsLine)
        {
            var parts = carDetailsLine.Split(' ');
            if (parts.Length != 3 || !int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y) ||
                !Enum.TryParse(parts[2], true, out Orientation orientation))
                throw new FormatException("Invalid format. Please enter position and orientation as 'X Y Orientation' (e.g., '1 2 N').");
            return  (x, y, orientation);
        }

        /// <summary>
        /// Parses a string of characters into a list of commands for controlling a car.
        /// </summary>
        /// <param name="commandsLine">The input string where each character represents a command ('L' for left, 'R' for right, 'F' for forward).</param>
        /// <returns>A list of <see cref="ICommand"/> objects corresponding to the parsed commands.</returns>
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
                    _ => throw new FormatException($"Invalid command '{commandChar}'. Only 'L', 'R', and 'F' are allowed.")
                };
                commands.Add(command);
            }
            return commands;
        }
    }
}

