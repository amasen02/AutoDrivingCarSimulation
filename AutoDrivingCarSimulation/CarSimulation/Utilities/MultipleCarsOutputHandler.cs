using CarSimulation.Interfaces;

namespace CarSimulation.Utilities
{
    /// <summary>
    /// Handles output for the multiple cars scenario to the console.
    /// </summary>
    public class MultipleCarsOutputHandler : IOutputHandler
    {
        /// <summary>
        /// Outputs the result of the multiple cars simulation to the console.
        /// </summary>
        /// <param name="result">The result to output.</param>
        public void OutputResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
