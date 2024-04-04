using CarSimulation.Models;

namespace CarSimulation.Simulation
{
    /// <summary>
    /// Handles the detection of collisions between cars in the simulation.
    /// </summary>
    public class CollisionHandler
    {
        /// <summary>
        /// Detects collisions based on the current positions of all cars in the simulation.
        /// </summary>
        /// <param name="cars">A collection of cars to check for collisions.</param>
        /// <returns>A list of collisions detected during the current simulation step.</returns>
        public List<Collision> DetectCollisions(Dictionary<string, Car> cars)
        {
            var collisions = new List<Collision>();
            var groupedPositions = cars.Values
                                        .GroupBy(car => car.Position)
                                        .Where(group => group.Count() > 1);

            foreach (var group in groupedPositions)
            {
                var collision = new Collision
                {
                    CarsInvolved = group.Select(car => car.Name).ToList(),
                    Position = group.Key,
                    Step = cars.Values.Max(car => car.CollisionStep) 
                };
                collisions.Add(collision);
            }

            return collisions;
        }
    }
}
