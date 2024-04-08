using CarSimulation.Interfaces;
using CarSimulation.Models;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="step">The current step of the simulation.</param>
        /// <returns>A list of collisions detected during the current simulation step.</returns>
        public List<Collision> DetectCollisions(Dictionary<string, Car> cars, int step)
        {
            var collisions = FindCollisions(cars);
            return CreateCollisionObjects(collisions, step);
        }

        /// <summary>
        /// Groups cars by their positions and filters groups with more than one car at the same position.
        /// </summary>
        /// <param name="cars">A dictionary containing cars with their unique names as keys.</param>
        /// <returns>Grouped cars where collisions occurred.</returns>
        private IEnumerable<IGrouping<(int X, int Y), Car>> FindCollisions(Dictionary<string, Car> cars)
        {
            return cars.Values.GroupBy(car => car.Position).Where(group => group.Count() > 1);
        }

        /// <summary>
        /// Creates collision objects for the detected collisions.
        /// </summary>
        /// <param name="collisions">Grouped cars where collisions occurred.</param>
        /// <param name="step">The current step of the simulation.</param>
        /// <returns>A list of collision objects representing detected collisions.</returns>
        private List<Collision> CreateCollisionObjects(IEnumerable<IGrouping<(int X, int Y), Car>> collisions, int step)
        {
            return collisions.SelectMany(group =>
                group.Select(car => new Collision
                {
                    CarsInvolved = group.Select(c => c.Name).ToList(),
                    Position = group.Key,
                    Step = step
                })).ToList();
        }
    }
}
