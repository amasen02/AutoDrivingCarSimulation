using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;
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
            var maxCommandsCount = GetMaxCommandsCount(simulationInput.CommandsPerCar);

            RunSimulationSteps(cars, simulationInput.CommandsPerCar, maxCommandsCount);
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
        /// Gets the maximum number of commands among all cars.
        /// </summary>
        /// <param name="commandsPerCar">The commands per car dictionary.</param>
        /// <returns>The maximum number of commands.</returns>
        private int GetMaxCommandsCount(Dictionary<string, List<ICommand>> commandsPerCar)
        {
            return commandsPerCar.Values.Max(c => c.Count);
        }

        /// <summary>
        /// Executes the commands for each car in parallel for each simulation step.
        /// </summary>
        /// <param name="cars">The cars participating in the simulation.</param>
        /// <param name="commandsPerCar">The commands to be executed for each car.</param>
        /// <param name="maxCommandsCount">The maximum number of commands among all cars.</param>
        private void RunSimulationSteps(Dictionary<string, Car> cars, Dictionary<string, List<ICommand>> commandsPerCar, int maxCommandsCount)
        {
            for (int step = 0; step < maxCommandsCount; step++)
            {
                ExecuteCommandsForStep(cars, commandsPerCar, step);
                if (DetectAndHandleCollisions(cars, step))
                {
                    return;
                }
            }
            HandleNoCollisions();
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
        /// Detects collisions between cars and handles them if detected.
        /// </summary>
        /// <param name="cars">The cars participating in the simulation.</param>
        /// <param name="step">The current step of the simulation.</param>
        /// <returns>True if a collision is detected and handled, otherwise false.</returns>
        private bool DetectAndHandleCollisions(Dictionary<string, Car> cars, int step)
        {
            var collisions = collisionDetector.DetectCollisions(cars, step + 1);
            if (collisions.Any())
            {
                HandleCollisions(collisions);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Handles collisions by formatting collision data and outputting the result.
        /// </summary>
        /// <param name="collisions">The collisions detected during the simulation.</param>
        private void HandleCollisions(List<Collision> collisions)
        {
            var collisionReport = FormatCollisions(collisions);
            outputHandler.OutputResult(collisionReport);
        }

        /// <summary>
        /// Handles the case when no collisions are detected during the simulation.
        /// </summary>
        private void HandleNoCollisions()
        {
            outputHandler.OutputResult(MessageConstants.NoCollisionMessage);
        }
        /// <summary>
        /// Formats the list of collisions for output.
        /// </summary>
        /// <param name="collisions">The collisions detected during the simulation.</param>
        /// <returns>A formatted string representing the collisions.</returns>
        private string FormatCollisions(List<Collision> collisions)
        {
            var collisionReport = new StringBuilder();
            var uniqueCollisions = GetUniqueCollisions(collisions);

            foreach (var collision in uniqueCollisions.OrderBy(c => c.Step))
            {
                AppendCollisionDetails(collisionReport, collision);
            }
            return collisionReport.ToString().TrimEnd();
        }

        /// <summary>
        /// Gets unique collisions based on their step.
        /// </summary>
        /// <param name="collisions">The list of collisions.</param>
        /// <returns>A collection of unique collisions.</returns>
        private IEnumerable<Collision> GetUniqueCollisions(List<Collision> collisions)
        {
            return collisions.GroupBy(c => c.Step).Select(group => group.First());
        }

        /// <summary>
        /// Appends collision details to the collision report.
        /// </summary>
        /// <param name="collisionReport">The string builder to append the collision details to.</param>
        /// <param name="collision">The collision to append details for.</param>
        private void AppendCollisionDetails(StringBuilder collisionReport, Collision collision)
        {
            collisionReport.AppendLine($"{string.Join(" ", collision.CarsInvolved)}\n{collision.Position.X} {collision.Position.Y}\n{collision.Step}\n");
            string collisionExplanation = string.Format(MessageConstants.CollisionExplanation, string.Join(" ", collision.CarsInvolved), collision.Position.X, collision.Position.Y, collision.Step);
            collisionReport.AppendLine(collisionExplanation);
        }
    }
}
