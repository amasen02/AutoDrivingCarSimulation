using CarSimulation.Enums;
using CarSimulation.Interfaces;

namespace CarSimulation.Models
{
    /// <summary>
    /// Represents a car in the simulation, capable of moving and changing orientation based on executed commands.
    /// </summary>
    public class Car : ICar
    {
        /// <summary>
        /// Gets the name of the car.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the position of the car as a tuple of X and Y coordinates.
        /// </summary>
        public (int X, int Y) Position { get; set; }

        /// <summary>
        /// Gets or sets the orientation of the car.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the car is currently colliding with another car.
        /// </summary>
        public bool IsColliding { get; set; }

        /// <summary>
        /// Gets or sets the simulation step number at which the collision occurred.
        /// </summary>
        public int CollisionStep { get; set; }

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
            IsColliding = false;
            CollisionStep = 0;
        }

        /// <summary>
        /// Applies the given command to the car if it is within the boundaries specified by the simulation field.
        /// Commands that would move the car out of bounds are ignored, ensuring the car remains within the simulation field.
        /// </summary>
        /// <param name="command">The command to apply to the car.</param>
        /// <param name="field">The simulation field, used to check if the command keeps the car within bounds.</param>
        public void ApplyCommand(ICommand command, Field field)
        {
            // Create a temporary copy of the car's state to apply the command to
            Car tempCar = new Car(this.Position.X, this.Position.Y, this.Orientation, this.Name);

            // Apply the command to the temporary car state
            command.Execute(tempCar);

            // Check if the temporary state is within the field boundaries
            if (field.IsInsideBounds(tempCar.Position))
            {
                // Update the actual car state only if the command does not move it out of bounds
                this.Position = tempCar.Position;
                this.Orientation = tempCar.Orientation;
            }
            // If the command moves the car out of bounds, it is ignored, and the car's state remains unchanged
        }
    }
}
