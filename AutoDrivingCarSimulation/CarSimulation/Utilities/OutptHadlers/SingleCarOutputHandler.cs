using CarSimulation.Interfaces;

namespace CarSimulation.Utilities.OutptHadlers
{
    /// <summary>
    /// Handles output for the single car scenario to the console.
    /// </summary>
    public class SingleCarOutputHandler : IOutputHandler
    {
        /// <summary>
        /// Outputs the result of the single car simulation to the console.
        /// </summary>
        /// <param name="result">The result to output.</param>
        public void OutputResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
