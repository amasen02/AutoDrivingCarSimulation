namespace CarSimulation.Interfaces
{
    /// <summary>
    /// Defines a contract for simulation handlers to run simulations.
    /// </summary>
    public interface ISimulationHandler
    {
        /// <summary>
        /// Runs the simulation based on the provided input and outputs the results.
        /// </summary>
        void RunSimulation();
    }
}
