using CarSimulation.Interfaces;
using CarSimulation.Simulation;

namespace CarSimulation.Factory
{
    public static class SimulationHandlerFactory
    {
        /// <summary>
        /// Creates an appropriate simulation handler based on the simulation type.
        /// </summary>
        /// <param name="isSingle">Indicates whether a single or multiple cars simulation is to be run.</param>
        /// <param name="inputHandler">The input handler for gathering simulation settings.</param>
        /// <param name="outputHandler">The output handler for displaying simulation results.</param>
        /// <returns>An instance of <see cref="ISimulationHandler"/> tailored to the selected simulation type.</returns>
        public static ISimulationHandler CreateSimulationHandler(bool isSingle, IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            return isSingle
                ? new SingleCarSimulationHandler(inputHandler, outputHandler)
                : new MultipleCarsSimulationHandler(inputHandler, outputHandler);
        }
    }
}
