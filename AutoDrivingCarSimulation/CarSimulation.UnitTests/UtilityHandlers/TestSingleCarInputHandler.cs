﻿using CarSimulation.Commands;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.UnitTests.UtilityHandlers
{
    /// <summary>
    /// Provides a test-specific implementation of the IInputHandler interface.
    /// This handler is used to supply predetermined simulation input data for testing purposes.
    /// </summary>
    public class TestSingleCarInputHandler : IInputHandler
    {
        /// <summary>
        /// Retrieves a predefined set of simulation inputs for testing.
        /// This includes the simulation field dimensions, the initial position and orientation of the car,
        /// and a sequence of commands for the car to execute.
        /// </summary>
        /// <returns>A SimulationInput object populated with test-specific data.</returns>
        public SimulationInput GetInput()
        {
            // Returning the specific simulation input for our test scenario
            return new SimulationInput(
                10, 10, // Field dimensions
                new List<CarInput>
                {
                new CarInput { Name = "TestCar", X = 1, Y = 2, Orientation = Orientation.N } // Initial car position and orientation
                },
                new Dictionary<string, List<ICommand>>
                { 
                    // Commands sequence for "TestCar"
                    { "TestCar", new List<ICommand>
                        {
                            new MoveForwardCommand(), new MoveForwardCommand(),
                            new TurnRightCommand(), new MoveForwardCommand(),
                            new MoveForwardCommand(), new TurnRightCommand(),
                            new MoveForwardCommand(), new TurnRightCommand(),
                            new TurnLeftCommand(), new MoveForwardCommand()
                        }
                    }
                }
            );
        }

        /// <summary>
        /// Executes a sequence of commands on a car, checking for and preventing movement outside the field boundaries.
        /// </summary>
        /// <param name="field">The simulation field.</param>
        /// <param name="car">The car executing the commands.</param>
        /// <param name="commands">The sequence of commands to execute.</param>
        public void ExecuteCommandSequence(Field field, Car car, string commands)
        {
            foreach (var commandChar in commands)
            {
                ICommand command = commandChar switch
                {
                    'F' => new MoveForwardCommand(),
                    'L' => new TurnLeftCommand(),
                    'R' => new TurnRightCommand(),
                    _ => null
                };

                var previousPosition = car.Position;
                command?.Execute(car);

                // If executing a command would move the car out of bounds, revert to the previous position.
                if (!field.IsInsideBounds(car.Position))
                {
                    car.Position = previousPosition;
                }
            }
        }
    }
}