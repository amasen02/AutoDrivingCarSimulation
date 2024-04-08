using CarSimulation.Utilities.Constants;

namespace CarSimulation.Models
{
    /// <summary>
    /// Represents the input data required to initialize a car for the simulation.
    /// </summary>
    public class CarInput
    {
        /// <summary>
        /// Gets or sets the name of the car.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the initial X-coordinate of the car.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the initial Y-coordinate of the car.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the initial orientation of the car.
        /// </summary>
        public Orientation Orientation { get; set; }
    }
}
