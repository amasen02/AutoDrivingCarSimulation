using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities;
using System.Text;

namespace CarSimulation.Simulation
{
    /// <summary>
    /// Manages simulations involving multiple cars, executing commands for each car in parallel and detecting collisions.
    /// </summary>
    public class MultipleCarsSimulationHandler : ISimulationHandler
    {
        private readonly IInputHandler inputHandler;
        private readonly IOutputHandler outputHandler;
        private readonly ICollisionDetector collisionDetector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCarsSimulationHandler"/> class.
        /// </summary>
        /// <param name="inputHandler">The input handler to gather simulation settings.</param>
        /// <param name="outputHandler">The output handler to display simulation results.</param>
        /// <param name="collisionDetector">The collision detector.</param>
        public MultipleCarsSimulationHandler(IInputHandler inputHandler, IOutputHandler outputHandler, ICollisionDetector collisionDetector)
        {
            this.inputHandler = inputHandler;
            this.outputHandler = outputHandler;
            this.collisionDetector = collisionDetector;
        }

        /// <summary>
        /// Executes the simulation for multiple cars based on input commands, checking for collisions at each step.
        /// </summary>
        public void RunSimulation()
        {
            var simulationInput = inputHandler.GetInput();
            var cars = InitializeCars(simulationInput.CarInputs);
            var maxCommandsCount = simulationInput.CommandsPerCar.Values.Max(c => c.Count);

            for (int step = 0; step < maxCommandsCount; step++)
            {
                ExecuteCommandsForStep(cars, simulationInput.CommandsPerCar, step);
                var allCollisions = collisionDetector.DetectCollisions(cars, step + 1 );
                if (allCollisions.Any())
                {
                    outputHandler.OutputResult(FormatCollisions(allCollisions));
                    return;
                }
            }

            outputHandler.OutputResult(Constants.NoCollisionMessage);
        }

        /// <summary>
        /// Initializes car objects based on provided input data.
        /// </summary>
        /// <param name="carInputs">List of car input data.</param>
        /// <returns>Dictionary of car names to car objects.</returns>
        private Dictionary<string, Car> InitializeCars(List<CarInput> carInputs)
        {
            return carInputs.ToDictionary(carInput => carInput.Name,
                                          carInput => new Car(carInput.X, carInput.Y, carInput.Orientation, carInput.Name));
        }

        /// <summary>
        /// Executes the commands for all cars for a given simulation step.
        /// </summary>
        /// <param name="cars">The cars participating in the simulation.</param>
        /// <param name="commandsPerCar">The commands to be executed for each car.</param>
        /// <param name="step">The current step of the simulation.</param>
        private void ExecuteCommandsForStep(Dictionary<string, Car> cars, Dictionary<string, List<ICommand>> commandsPerCar, int step)
        {
            foreach (var carName in commandsPerCar.Keys)
            {
                if (step < commandsPerCar[carName].Count)
                {
                    var command = commandsPerCar[carName][step];
                    command.Execute(cars[carName]);
                }
            }
        }

        /// <summary>
        /// Formats the list of collisions for output.
        /// </summary>
        /// <param name="collisions">The collisions detected during the simulation.</param>
        /// <returns>A formatted string representing the collisions.</returns>
        private string FormatCollisions(List<Collision> collisions)
        {
            var collisionReport = new StringBuilder();
            foreach (var collision in collisions.OrderBy(c => c.Step))
            {
                collisionReport.AppendLine($"{string.Join(" ", collision.CarsInvolved)}\n{collision.Position.X} {collision.Position.Y}\n{collision.Step}\n");
            }
            return collisionReport.ToString().TrimEnd();
        }
    }
}
