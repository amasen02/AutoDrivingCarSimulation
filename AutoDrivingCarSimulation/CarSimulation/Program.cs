using CarSimulation.Factory;
using CarSimulation.Interfaces;
using CarSimulation.Utilities;

namespace AutoDrivingCarSimulation
{
    /// <summary>
    /// The main program class for running car simulations.
    /// Allows the user to choose between single and multiple car simulations.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the program. Displays a welcome message and starts the simulation choice loop.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            WelcomeDisplay();
            RunSimulationChoiceLoop();
        }

        /// <summary>
        /// Displays a welcome message to the console.
        /// </summary>
        private static void WelcomeDisplay()
        {
            Console.WriteLine("         Welcome to the Car Simulation Program!\n\n");
            // Your ASCII art here
            Console.WriteLine("             __..-======-------..__");
            Console.WriteLine("          . '    ______    ___________`.");
            Console.WriteLine("        .' .--. '.-----.`. `.-----.-----`.");
            Console.WriteLine("       / .'   | ||      `.` \\     \\     \\            _");
            Console.WriteLine("     .' /     | ||        \\ \\_____\\_____\\__________[_]");
            Console.WriteLine("    /   `-----' |'---------`\\  .'                       \\");
            Console.WriteLine("   /============|============\\'-------------------.._____|");
            Console.WriteLine(".-`---.         |-==.        |'.__________________  =====|-._");
            Console.WriteLine(".`        `.      |            |      .--------.    _` ====|  _ .");
            Console.WriteLine("/     __     \\     |            |   .'           `. [_] `.==| [_] \\");
            Console.WriteLine("[   .`    `.  |     |            | .'     .---.     \\      \\=|     |");
            Console.WriteLine("|  | / .-. '  |_____\\___________/_/     .'---. `.    |     | |     |");
            Console.WriteLine(" `-'| | O |'..`------------------'.....'/ .-. \\ |    |       ___.--'");
            Console.WriteLine("     \\ `-' / /   `._.'                 | | O | |'___...----''___.--'");
            Console.WriteLine("      `._.'.'                           \\ `-' / [___...----''_.']");
            Console.WriteLine("                                         `._.'.'");
            Console.WriteLine("\n   Prepare for a high-speed simulation challenge!");
        }

        /// <summary>
        /// Continuously prompts the user for their simulation choice until they decide to exit.
        /// </summary>
        private static void RunSimulationChoiceLoop()
        {
            string choice;
            do
            {
                choice = GetUserChoice();
                switch (choice)
                {
                    case "1":
                        RunSimulation(true);
                        break;
                    case "2":
                        RunSimulation(false);
                        break;
                }
            }
            while (choice.ToLower() != "q");
        }

        /// <summary>
        /// Prompts the user to choose the type of simulation or to exit the program.
        /// </summary>
        /// <returns>The user's choice as a string.</returns>
        private static string GetUserChoice()
        {
            Console.WriteLine("\nChoose simulation type:");
            Console.WriteLine("1. Single Car Simulation");
            Console.WriteLine("2. Multiple Cars Simulation");
            Console.WriteLine("Type 'q' to exit.");
            Console.Write("Enter your choice (1 or 2): ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Initializes and runs the simulation based on the specified simulation type.
        /// </summary>
        /// <param name="isSingle">Determines whether a single car or multiple cars simulation should be run.</param>
        private static void RunSimulation(bool isSingle)
        {
            IInputHandler inputHandler = isSingle ? new SingleCarInputHandler() : new MultipleCarsInputHandler();
            IOutputHandler outputHandler = isSingle ? new SingleCarOutputHandler() : new MultipleCarsOutputHandler();
            ISimulationHandler simulationHandler = SimulationHandlerFactory.CreateSimulationHandler(isSingle, inputHandler, outputHandler);
            TryRunSimulation(simulationHandler);
        }

        /// <summary>
        /// Tries to run the simulation, catching and reporting any input errors.
        /// </summary>
        /// <param name="simulationHandler">The simulation handler responsible for executing the simulation.</param>
        private static void TryRunSimulation(ISimulationHandler simulationHandler)
        {
            try
            {
                simulationHandler.RunSimulation();
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
        }
    }
}
