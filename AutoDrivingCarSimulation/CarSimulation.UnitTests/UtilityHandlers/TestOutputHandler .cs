using CarSimulation.Interfaces;

namespace CarSimulation.UnitTests.UtilityHandlers
{
    /// <summary>
    /// A test-specific implementation of the IOutputHandler interface designed to capture simulation results.
    /// This handler stores the last output result, allowing for assertions in test scenarios.
    /// </summary>
    public class TestOutputHandler : IOutputHandler
    {
        /// <summary>
        /// Gets the last output result produced by the simulation. This property is set each time OutputResult is called.
        /// </summary>
        public string LastOutput { get; private set; }

        /// <summary>
        /// Captures the simulation output, storing it in the LastOutput property for later retrieval and verification.
        /// </summary>
        /// <param name="result">The result string produced by the simulation, which may describe a collision event or the lack thereof.</param>
        public void OutputResult(string result)
        {
            LastOutput = result;
        }
    }
}
