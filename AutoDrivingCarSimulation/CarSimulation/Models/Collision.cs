namespace CarSimulation.Models
{
    /// <summary>
    /// Represents a collision event between two or more cars in the simulation.
    /// </summary>
    public class Collision
    {
        /// <summary>
        /// Gets or sets the names of the cars involved in the collision.
        /// </summary>
        public List<string> CarsInvolved { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the position (X, Y coordinates) where the collision occurred.
        /// </summary>
        public (int X, int Y) Position { get; set; }

        /// <summary>
        /// Gets or sets the simulation step number at which the collision occurred.
        /// </summary>
        public int Step { get; set; }
    }
}
