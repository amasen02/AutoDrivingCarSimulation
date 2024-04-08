using CarSimulation.Interfaces;
using CarSimulation.Models;
using CarSimulation.Utilities.Constants;
using CarSimulation.Utilities.Extensions;
using System.Text;

namespace CarSimulation.Simulation
{
    /// <summary>
    /// Manages the simulation for scenarios involving a single car. 
    /// It processes commands to control the car's movement and orientation within the simulation field.
    /// </summary>
    public class SingleCarSimulationHandler : ISimulationHandler
    {
        private readonly IOutputHandler outputHandler;
        private readonly Field field;
        private readonly SimulationInput simulationInput;

        /// <summary>
        /// Initializes a new instance of the SingleCarSimulationHandler class.
        /// </summary>
        /// <param name="inputHandler">Responsible for providing the simulation input.</param>
        /// <param name="outputHandler">Responsible for outputting the simulation results.</param>
        public SingleCarSimulationHandler(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            this.outputHandler = outputHandler;
            simulationInput = inputHandler.GetInput();
            field = new Field(simulationInput.Width, simulationInput.Height);
        }

        /// <summary>
        /// Executes the simulation for a single car.
        /// </summary>
        public void RunSimulation()
        {
            var carInput = simulationInput.CarInputs.First();
            var car = InitializeCar(carInput);
            ExecuteCommands(car, simulationInput.CommandsPerCar.Values.First());
            OutputFinalPosition(car);
        }

        /// <summary>
        /// Initializes the car using the provided input data.
        /// </summary>
        /// <param name="carInput">Input data for the car.</param>
        /// <returns>The initialized car.</returns>
        private Car InitializeCar(CarInput carInput)
        {
            return new Car(carInput.X, carInput.Y, carInput.Orientation);
        }

        /// <summary>
        /// Executes each command for the car.
        /// </summary>
        /// <param name="car">The car to execute commands for.</param>
        /// <param name="commands">The commands to be executed.</param>
        private void ExecuteCommands(Car car, List<ICommand> commands)
        {
            foreach (var command in commands)
            {
                ExecuteSingleCommand(car, command);
                if (!IsInsideBounds(car))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Executes a single command for the car.
        /// </summary>
        /// <param name="car">The car to execute the command for.</param>
        /// <param name="command">The command to be executed.</param>
        private void ExecuteSingleCommand(Car car, ICommand command)
        {
            command.Execute(car);
        }

        /// <summary>
        /// Checks if the car is inside the simulation field bounds.
        /// </summary>
        /// <param name="car">The car to check.</param>
        /// <returns>True if the car is inside the bounds, otherwise false.</returns>
        private bool IsInsideBounds(Car car)
        {
            return field.IsInsideBounds(car.Position);
        }

        /// <summary>
        /// Outputs the final position and orientation of the car.
        /// </summary>
        /// <param name="car">The car whose final position and orientation are to be output.</param>
        private void OutputFinalPosition(Car car)
        {
            StringBuilder outputReport = new StringBuilder();
            outputReport.AppendLine($"{car.Position.X} {car.Position.Y} {car.Orientation}\n");
            string outputExplanation = string.Format(MessageConstants.SingleCarOutputExplanation, car.Position.X, car.Position.Y, car.Orientation.ToShorthandString());
            outputReport.AppendLine(outputExplanation);
            outputHandler.OutputResult(outputReport.ToString());
        }
    }
}
