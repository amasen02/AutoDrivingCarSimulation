using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Simulation
{
    /// <summary>
    /// Handles the detection of collisions between cars in the simulation.
    /// </summary>
    public class CollisionDetector : ICollisionDetector
    {
        /// <summary>
        /// Detects collisions based on the current positions of all cars in the simulation.
        /// </summary>
        /// <param name="cars">A dictionary containing cars with their unique names as keys.</param>
        /// <param name="cars">Index of the command which is executing<.</param>
        /// <returns>A list of collisions detected during the current simulation step.</returns>
        public List<Collision> DetectCollisions(Dictionary<string, Car> cars, int step)
        {
            // Initialize the list to store detected collisions
            var collisions = new List<Collision>();

            // Group cars by their positions and filter groups with more than one car at the same position
            var groupedPositions = cars.Values
                                        .GroupBy(car => car.Position)
                                        .Where(group => group.Count() > 1);

            // Iterate through groups of cars at the same position to create Collision objects
            foreach (var group in groupedPositions)
            {
                // Create a new Collision object for the current group
                var collision = new Collision
                {
                    // List the names of cars involved in the collision
                    CarsInvolved = group.Select(car => car.Name).ToList(),
                    // Set the position of the collision
                    Position = group.Key,
                    // Determine the simulation step when the collision occurred
                    Step = step
                };
                // Add the collision to the list of collisions
                collisions.Add(collision);
            }

            // Return the list of detected collisions
            return collisions;
        }
    }
}
