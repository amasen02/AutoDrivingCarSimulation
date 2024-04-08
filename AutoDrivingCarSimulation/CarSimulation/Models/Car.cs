using CarSimulation.Enums;
using CarSimulation.Interfaces;

namespace CarSimulation.Models
{
    /// <summary>
    /// Represents a car in the simulation.
    /// </summary>
    public class Car : ICar
    {
        /// <summary>
        /// Gets or sets the name of the car.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the position of the car.
        /// </summary>
        public (int X, int Y) Position { get; set; }

        /// <summary>
        /// Gets or sets the orientation of the car.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Initializes a new instance of the Car class with specified position, orientation, and optionally a name.
        /// </summary>
        /// <param name="x">The initial X-coordinate of the car.</param>
        /// <param name="y">The initial Y-coordinate of the car.</param>
        /// <param name="orientation">The initial orientation of the car.</param>
        /// <param name="name">The name of the car (optional).</param>
        public Car(int x, int y, Orientation orientation, string name = "UnnamedCar")
        {
            Position = (x, y);
            Orientation = orientation;
            Name = name;
        }

        /// <summary>
        /// Applies the given command to the car if it is within the boundaries specified by the simulation field.
        /// </summary>
        /// <param name="command">The command to apply to the car.</param>
        /// <param name="field">The simulation field, used to check if the command keeps the car within bounds.</param>
        public void ApplyCommand(ICommand command, Field field)
        {
            var newPosition = command.GetNewPosition(Position, Orientation);
            if (field.IsInsideBounds(newPosition))
            {
                UpdateCarState(newPosition, command.GetNewOrientation(Orientation));
            }
        }

        private void UpdateCarState((int X, int Y) newPosition, Orientation newOrientation)
        {
            Position = newPosition;
            Orientation = newOrientation;
        }
    }
}
