using CarSimulation.Commands;
using CarSimulation.Enums;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Simulation;
using CarSimulation.UnitTests.UtilityHandlers;

namespace CarSimulation.UnitTests.Tests
{
    /// <summary>
    /// Contains NUnit tests for the MultipleCarsSimulationHandler, focusing on simulations with multiple cars
    /// to ensure accurate command execution, collision detection, and reporting.
    /// </summary>
    [TestFixture]
    public class MultipleCarsSimulationTests
    {
        /// <summary>
        /// Verifies that the simulation accurately reports no collision when two cars are given command sequences
        /// that do not lead to their intersection within the simulation field.
        /// </summary>
        [Test]
        public void Simulation_ShouldReportNoCollisionWhenCarsDoNotIntersect()
        {
            // Arrange
            var inputHandler = new TestMultipleCarsInputHandler
            {
                CarInputsOverride = new List<CarInput>
                {
                    new CarInput { Name = "A", X = 0, Y = 0, Orientation = Orientation.N },
                    new CarInput { Name = "B", X = 3, Y = 3, Orientation = Orientation.E }
                },
                CommandsOverride = new Dictionary<string, List<ICommand>>
                {
                    { "A", ParseCommands("RLRLRL") },
                    { "B", ParseCommands("RLRLRL") }
                }
            };
            var collisionDetector = new CollisionDetector();
            var outputHandler = new TestOutputHandler();
            var simulationHandler = new MultipleCarsSimulationHandler(inputHandler, outputHandler, collisionDetector);

            // Act
            simulationHandler.RunSimulation();

            // Assert
            Assert.That(outputHandler.LastOutput, Is.EqualTo("No collision"), "The simulation incorrectly reported a collision.");
        }

        /// <summary>
        /// Tests the simulation's ability to handle and correctly report collisions between two cars.
        /// </summary>
        [Test]
        public void Simulation_WithTwoCars_CollisionBetween2Cars()
        {
            // Arrange
            var inputHandler = new TestMultipleCarsInputHandler
            {
                CarInputsOverride = new List<CarInput>
                    {
                        new CarInput { Name = "A", X = 1, Y = 2, Orientation = Orientation.N },
                        new CarInput { Name = "B", X = 7, Y = 8, Orientation = Orientation.W },
                    },
                CommandsOverride = new Dictionary<string, List<ICommand>>
                    {
                        { "A", ParseCommands("FFRFFFFRRL") },
                        { "B", ParseCommands("FFLFFFFFFF") }
                    }
            };
            var collisionDetector = new CollisionDetector();
            var outputHandler = new TestOutputHandler();
            var simulationHandler = new MultipleCarsSimulationHandler(inputHandler, outputHandler, collisionDetector);

            // Act
            simulationHandler.RunSimulation();

            // Assert
            Assert.AreEqual("A B\n5 4\n7", outputHandler.LastOutput, "The simulation failed to report a collision between the two cars.");
        }

        /// <summary>
        /// Tests the simulation's ability to handle and correctly report collisions between more than 2 cars at the same position.
        /// </summary>
        [Test]
        public void Simulation_WithMultiCars_CollisionBetweenMoreThan2CarsInSamePostion()
        {
            // Arrange
            var inputHandler = new TestMultipleCarsInputHandler
            {
                CarInputsOverride = new List<CarInput>
                    {
                        new CarInput { Name = "A", X = 1, Y = 2, Orientation = Orientation.N },
                        new CarInput { Name = "B", X = 7, Y = 8, Orientation = Orientation.W },
                        new CarInput { Name = "C", X = 5, Y = 3, Orientation = Orientation.N },
                    },
                CommandsOverride = new Dictionary<string, List<ICommand>>
                    {
                        { "A", ParseCommands("FFRFFFFRRL") },
                        { "B", ParseCommands("FFLFFFFFFF") },
                        { "C", ParseCommands("LRLRLRFLRL") }
                    }
            };
            var collisionDetector = new CollisionDetector();
            var outputHandler = new TestOutputHandler();
            var simulationHandler = new MultipleCarsSimulationHandler(inputHandler, outputHandler, collisionDetector);

            // Act
            simulationHandler.RunSimulation();

            // Assert
            Assert.AreEqual("A B C\n5 4\n7", outputHandler.LastOutput, "The simulation failed to report a collision between the two cars.");
        } 

        /// <summary>
        /// Parses a string of command characters ('F', 'R', 'L') into a corresponding list of ICommand objects.
        /// </summary>
        /// <param name="commandString">The string representing the sequence of commands.</param>
        /// <returns>A list of ICommand objects that can be executed by the simulation.</returns>
        private List<ICommand> ParseCommands(string commandString)
        {
            var commands = new List<ICommand>();
            foreach (char command in commandString)
            {
                ICommand commandItem = command switch
                {
                    'F' => new MoveForwardCommand(),
                    'R' => new TurnRightCommand(),
                    'L' => new TurnLeftCommand(),
                    _ => throw new ArgumentException($"Unsupported command: {command}")
                };
                commands.Add(commandItem);
            }
            return commands;
        }
    }
}
