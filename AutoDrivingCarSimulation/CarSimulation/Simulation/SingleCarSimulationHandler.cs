using CarSimulation.Interfaces;
using CarSimulation.Models;

namespace CarSimulation.Simulation
{
    /// <summary>
    /// Manages the simulation for scenarios involving a single car. 
    /// It processes commands to control the car's movement and orientation within the simulation field.
    /// </summary>
    public class SingleCarSimulationHandler : ISimulationHandler
    {
        private readonly IInputHandler inputHandler;
        private readonly IOutputHandler outputHandler;

        /// <summary>
        /// Initializes a new instance of the SingleCarSimulationHandler class.
        /// </summary>
        /// <param name="inputHandler">Responsible for providing the simulation input.</param>
        /// <param name="outputHandler">Responsible for outputting the simulation results.</param>
        public SingleCarSimulationHandler(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            this.inputHandler = inputHandler;
            this.outputHandler = outputHandler;
        }

        /// <summary>
        /// Executes the simulation for a single car, applying a sequence of commands to navigate the car within the simulation field.
        /// </summary>
        public void RunSimulation()
        {
            // Retrieve the simulation input data
            var simulationInput = inputHandler.GetInput();
            var field = new Field(simulationInput.Width, simulationInput.Height);
            var carInput = simulationInput.CarInputs.First();

            // Initialize the car using the input data
            Car car = new Car(carInput.X, carInput.Y, carInput.Orientation);

            // Execute each command for the car
            foreach (var command in simulationInput.CommandsPerCar.Values.First())
            {
                command.Execute(car);

                // Check if the car remains within the bounds after command execution
                if (!field.IsInsideBounds(car.Position))
                {
                    // If a command moves the car out of bounds, ignore it and do not update the car's position
                    break;
                }
            }

            // Output the final position and orientation of the car
            outputHandler.OutputResult($"{car.Position.X} {car.Position.Y} {car.Orientation}");
        }
    }
}