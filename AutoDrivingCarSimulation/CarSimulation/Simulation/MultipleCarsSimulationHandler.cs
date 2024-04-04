using CarSimulation.Interfaces;
using CarSimulation.Models;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleCarsSimulationHandler"/> class.
        /// </summary>
        /// <param name="inputHandler">The input handler to gather simulation settings.</param>
        /// <param name="outputHandler">The output handler to display simulation results.</param>
        public MultipleCarsSimulationHandler(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            this.inputHandler = inputHandler;
            this.outputHandler = outputHandler;
        }

        /// <summary>
        /// Executes the simulation for multiple cars based on input commands, checking for collisions at each step.
        /// </summary>
        public void RunSimulation()
        {
            var simulationInput = inputHandler.GetInput();
            var cars = InitializeCars(simulationInput.CarInputs);
            var allCollisions = new List<(List<string>, (int, int), int)>();

            for (int step = 0; step < simulationInput.CommandsPerCar.Values.Max(c => c.Count); step++)
            {
                ExecuteCommandsForStep(cars, simulationInput.CommandsPerCar, step);
                allCollisions.AddRange(CheckForCollisions(cars, step + 1));
            }

            outputHandler.OutputResult(allCollisions.Any() ? FormatCollisions(allCollisions) : "no collision");
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
        /// Checks for collisions between cars at the current step of the simulation.
        /// </summary>
        /// <param name="cars">The cars participating in the simulation.</param>
        /// <param name="step">The step number at which to check for collisions.</param>
        /// <returns>A list of collision events.</returns>
        private List<(List<string> CarsInvolved, (int X, int Y) Position, int Step)> CheckForCollisions(Dictionary<string, Car> cars, int step)
        {
            return cars.Values
                       .GroupBy(car => car.Position)
                       .Where(group => group.Count() > 1)
                       .Select(group => (group.Select(car => car.Name).ToList(), group.Key, step))
                       .ToList();
        }

        /// <summary>
        /// Formats the list of collisions for output.
        /// </summary>
        /// <param name="collisions">The collisions detected during the simulation.</param>
        /// <returns>A formatted string representing the collisions.</returns>
        private string FormatCollisions(List<(List<string> CarsInvolved, (int X, int Y) Position, int Step)> collisions)
        {
            StringBuilder collisionReport = new StringBuilder();
            foreach (var collision in collisions.OrderBy(c => c.Step))
            {
                collisionReport.AppendLine($"{string.Join(" ", collision.CarsInvolved)}\n{collision.Position.X} {collision.Position.Y}\n{collision.Step}\n");
            }
            return collisionReport.ToString().TrimEnd();
        }
    }
}
