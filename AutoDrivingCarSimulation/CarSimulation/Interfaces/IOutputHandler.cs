namespace CarSimulation.Interfaces
{
    /// <summary>
    /// Defines the contract for handling simulation output.
    /// </summary>
    public interface IOutputHandler
    {
        /// <summary>
        /// Outputs the simulation result.
        /// </summary>
        /// <param name="result">The result to output.</param>
        void OutputResult(string result);
    }
}

