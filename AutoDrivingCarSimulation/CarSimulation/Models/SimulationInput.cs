using CarSimulation.Interfaces;

namespace CarSimulation.Models
{
    /// <summary>
    /// Represents the input required to run the simulation, including the simulation field dimensions,
    /// car inputs, and the commands to be executed for each car.
    /// </summary>
    public class SimulationInput
    {
        /// <summary>
        /// Gets or sets the width of the simulation field.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the simulation field.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the list of car inputs, each representing initial settings for a car participating in the simulation.
        /// </summary>
        public List<CarInput> CarInputs { get; set; } = new List<CarInput>();

        /// <summary>
        /// Gets or sets the dictionary of commands to be executed for each car.
        /// The key is the name of the car, and the value is a list of commands for that car.
        /// </summary>
        /// <remarks>In a single car simulation, this dictionary may contain commands for just one car.</remarks>
        public Dictionary<string, List<ICommand>> CommandsPerCar { get; set; } = new Dictionary<string, List<ICommand>>();

        /// <summary>
        /// Initializes a new instance of the SimulationInput class with specified parameters.
        /// </summary>
        /// <param name="timeStep">The total number of steps in the simulation.</param>
        /// <param name="width">The width of the simulation field.</param>
        /// <param name="height">The height of the simulation field.</param>
        /// <param name="carInputs">The initial settings for cars in the simulation.</param>
        /// <param name="commandsPerCar">The commands to be executed for each car.</param>
        public SimulationInput(int width, int height, List<CarInput> carInputs, Dictionary<string, List<ICommand>> commandsPerCar)
        {
            Width = width;
            Height = height;
            CarInputs = carInputs;
            CommandsPerCar = commandsPerCar;
        }
    }
}
