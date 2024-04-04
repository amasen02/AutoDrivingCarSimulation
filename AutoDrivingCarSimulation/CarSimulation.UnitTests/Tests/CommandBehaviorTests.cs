using CarSimulation.Models;
using CarSimulation.Commands;
using CarSimulation.Enums;

namespace CarSimulation.UnitTests.Tests
{
    /// <summary>
    /// Tests for verifying the behavior of individual commands (MoveForward, TurnLeft, TurnRight)
    /// when applied to a Car instance.
    /// </summary>
    [TestFixture]
    public class CommandBehaviorTests
    {
        private Car _car;

        [SetUp]
        public void SetUp()
        {
            // Reset the car's state before each test
            _car = new Car(5, 5, Orientation.N);
        }

        /// <summary>
        /// Verifies that executing the MoveForwardCommand changes the car's position according to its orientation.
        /// </summary>
        [Test]
        public void MoveForwardCommand_Executed_CarMovesAccordingToOrientation()
        {
            var command = new MoveForwardCommand();
            command.Execute(_car);
            Assert.AreEqual((5, 6), _car.Position, "The MoveForwardCommand did not correctly move the car north.");
        }

        /// <summary>
        /// Verifies that executing the TurnLeftCommand changes the car's orientation counterclockwise.
        /// </summary>
        [Test]
        public void TurnLeftCommand_Executed_CarOrientationChangesCounterclockwise()
        {
            var command = new TurnLeftCommand();
            command.Execute(_car);
            Assert.AreEqual(Orientation.W, _car.Orientation, "The TurnLeftCommand did not correctly change the car's orientation to west.");
        }

        /// <summary>
        /// Verifies that executing the TurnRightCommand changes the car's orientation clockwise.
        /// </summary>
        [Test]
        public void TurnRightCommand_Executed_CarOrientationChangesClockwise()
        {
            var command = new TurnRightCommand();
            command.Execute(_car);
            Assert.AreEqual(Orientation.E, _car.Orientation, "The TurnRightCommand did not correctly change the car's orientation to east.");
        }
    }
}
