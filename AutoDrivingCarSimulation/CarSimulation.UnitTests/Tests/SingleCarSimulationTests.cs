using CarSimulation.Commands;
using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.UnitTests.UtilityHandlers;
using CarSimulation.Utilities.Constants;

namespace CarSimulation.UnitTests.Tests
{
    [TestFixture]
    public class SingleCarSimulationTests
    {
        private TestSingleCarInputHandler inputHandler;

        [SetUp]
        public void Setup()
        {
            inputHandler = new TestSingleCarInputHandler();
        }

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

            inputHandler.ExecuteCommandSequence(testField, car, commands);

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
            inputHandler.ExecuteCommandSequence(testField, car, commands);

            // Assert
            Assert.That(car.Position, Is.EqualTo((1, 0)), "The car's final position is incorrect.");
            Assert.That(car.Orientation, Is.EqualTo(Orientation.E), "The car's final orientation is incorrect.");
        }
    }
}
