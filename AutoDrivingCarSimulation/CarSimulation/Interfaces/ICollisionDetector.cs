using CarSimulation.Models;

namespace CarSimulation.Interfaces
{
    /// <summary>
    /// Represents a collision detector.
    /// </summary>
    public interface ICollisionDetector
    {
        /// <summary>
        /// Detects collisions based on the current positions of all cars in the simulation.
        /// </summary>
        /// <param name="cars">A dictionary containing cars with their unique names as keys.</param>
        /// <param name="step">Index of the command which is executing.</param>
        /// <returns>A list of collisions detected during the current simulation step.</returns>
        List<Collision> DetectCollisions(Dictionary<string, Car> cars, int step);
    }
}
