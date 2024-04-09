using CarSimulation.Commands;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Simulation;
using CarSimulation.UnitTests.UtilityHandlers;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.UnitTests.Tests
{
    /// <summary>
    /// Contains NUnit tests for the MultipleCarsSimulationHandler, focusing on simulations with multiple cars
    /// to ensure accurate command execution, collision detection, and reporting.
    /// </summary>
    [TestFixture]
    public class MultipleCarsSimulationTests
    {
        private TestOutputHandler outputHandler = new TestOutputHandler();
        private CollisionDetector collisionDetector = new CollisionDetector();
        private TestMultipleCarsInputHandler? inputHandler;
        private MultipleCarsSimulationHandler? simulationHandler;

        /// <summary>
        /// Performs setup actions before each test method execution.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            outputHandler = new TestOutputHandler();
            collisionDetector = new CollisionDetector();
        }

        /// <summary>
        /// Verifies that the simulation accurately reports no collision when two cars are given command sequences
        /// that do not lead to their intersection within the simulation field.
        /// </summary>
        [Test]
        public void Simulation_ShouldReportNoCollisionWhenCarsDoNotIntersect()
        {
            // Arrange
            inputHandler = new TestMultipleCarsInputHandler
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

            simulationHandler = new MultipleCarsSimulationHandler(inputHandler, outputHandler, collisionDetector);

            // Act
            var collisions = simulationHandler.RunSimulationAndReturnCollisions(InitializeCars(inputHandler.CarInputsOverride), inputHandler.CommandsOverride, GetMaxCommandsCount(inputHandler.CommandsOverride));

            // Assert
            Assert.That(collisions, Is.Empty, "No collision should be detected");

        }

        /// <summary>
        /// Tests the simulation's ability to handle and correctly report collisions between two cars.
        /// </summary>
        [Test]
        public void Simulation_WithTwoCars_CollisionBetween2Cars()
        {
            // Arrange
            inputHandler = new TestMultipleCarsInputHandler
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

            simulationHandler = new MultipleCarsSimulationHandler(inputHandler, outputHandler, collisionDetector);

            // Act
            var collisions = simulationHandler.RunSimulationAndReturnCollisions(InitializeCars(inputHandler.CarInputsOverride), inputHandler.CommandsOverride, GetMaxCommandsCount(inputHandler.CommandsOverride));

            // Assert
            Assert.That(collisions, Has.Count.GreaterThan(0), "Collision should be detected");

            foreach (var collision in collisions)
            {
                // Check the collision details
                Assert.That(collision.Position.X, Is.EqualTo(5), "Incorrect X position of collision");
                Assert.That(collision.Position.Y, Is.EqualTo(4), "Incorrect Y position of collision");
                Assert.That(collision.Step, Is.EqualTo(7), "Incorrect step of collision");

                // Check the names of the cars involved in the collision
                Assert.That(collision.CarsInvolved, Does.Contain("A"), "Car A should be involved in collision");
                Assert.That(collision.CarsInvolved, Does.Contain("B"), "Car B should be involved in collision");
            }
        }

        /// <summary>
        /// Tests the simulation's ability to handle and correctly report collisions between more than 2 cars at the same position.
        /// </summary>
        [Test]
        public void Simulation_WithMultiCars_CollisionBetweenMoreThan2CarsInSamePosition()
        {
            // Arrange
            inputHandler = new TestMultipleCarsInputHandler
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

            simulationHandler = new MultipleCarsSimulationHandler(inputHandler, outputHandler, collisionDetector);

            // Act
            var collisions = simulationHandler.RunSimulationAndReturnCollisions(InitializeCars(inputHandler.CarInputsOverride), inputHandler.CommandsOverride, GetMaxCommandsCount(inputHandler.CommandsOverride));

            // Assert
            Assert.That(collisions, Has.Count.GreaterThan(0), "Collision should be detected");

            foreach (var collision in collisions)
            {
                // Check the collision details
                Assert.That(collision.Position.X, Is.EqualTo(5), "Incorrect X position of collision");
                Assert.That(collision.Position.Y, Is.EqualTo(4), "Incorrect Y position of collision");
                Assert.That(collision.Step, Is.EqualTo(7), "Incorrect step of collision");

                // Check the names of the cars involved in the collision
                Assert.That(collision.CarsInvolved, Does.Contain("A"), "Car A should be involved in collision");
                Assert.That(collision.CarsInvolved, Does.Contain("B"), "Car B should be involved in collision");
                Assert.That(collision.CarsInvolved, Does.Contain("C"), "Car C should be involved in collision");
            }
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

        /// <summary>
        /// Initializes car objects based on provided input data.
        /// </summary>
        /// <param name="carInputs">List of car input data.</param>
        /// <returns>Dictionary of car names to car objects.</returns>
        private Dictionary<string, Car> InitializeCars(List<CarInput> carInputs)
        {
            return carInputs.ToDictionary(carInput => carInput.Name,
                                          carInput => new Car(carInput.X, carInput.Y, carInput.Orientation, carInput.Name));
        }

        /// <summary>
        /// Gets the maximum number of commands among all cars.
        /// </summary
        /// <param name="commandsPerCar">The commands per car dictionary.</param>
        /// <returns>The maximum number of commands.</returns>
        private int GetMaxCommandsCount(Dictionary<string, List<ICommand>> commandsPerCar)
        {
            return commandsPerCar.Values.Max(c => c.Count);
        }
    }
}
