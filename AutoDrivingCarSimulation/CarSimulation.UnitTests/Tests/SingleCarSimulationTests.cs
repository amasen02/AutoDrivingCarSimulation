using CarSimulation.Commands;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.UnitTests.Tests
{
    [TestFixture]
    public class SingleCarSimulationTests
    {
        /// <summary>
        /// Tests the scenario where a single car moves according to a specified command sequence
        /// and validates the final position and orientation, ensuring the car does not exit the field boundaries.
        /// </summary>
        [Test]
        public void Simulation_SingleCar_SequenceTestingWithInputs()
        {
            // Arrange
            Field testField = new Field(10, 10);
            Car car = new Car(1, 2, Orientation.N);
            var commands = "FFRFFFRRLF";

            ExecuteCommandSequence(testField, car, commands);

            // Assert
            Assert.That(car.Position, Is.EqualTo((4, 3)), "The car's final position is incorrect.");
            Assert.That(car.Orientation, Is.EqualTo(Orientation.S), "The car's final orientation is incorrect.");
        }

        /// <summary>
        /// Tests the scenario where a single car moves according to a specified command sequence
        /// and validates the final position and orientation, ensuring the car does not exit the field boundaries.
        /// </summary>
        [Test]
        public void Simulation_SingleCar_ShouldIgnoreOutOfBoundsCommands()
        {
            // Arrange
            Field testField = new Field(10, 10);
            Car car = new Car(0, 0, Orientation.S);
            var commands = "FFFFFFLF";

            // Act
            ExecuteCommandSequence(testField, car, commands);

            // Assert
            Assert.That(car.Position, Is.EqualTo((1, 0)), "The car's final position is incorrect.");
            Assert.That(car.Orientation, Is.EqualTo(Orientation.E), "The car's final orientation is incorrect.");
        }

        /// <summary>
        /// Executes a sequence of commands on a car, checking for and preventing movement outside the field boundaries.
        /// </summary>
        /// <param name="field">The simulation field.</param>
        /// <param name="car">The car executing the commands.</param>
        /// <param name="commands">The sequence of commands to execute.</param>
        private void ExecuteCommandSequence(Field field, Car car, string commands)
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
