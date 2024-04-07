namespace CarSimulation.Utilities
{
    /// <summary>
    /// Provides a centralized location for storing constant string values used throughout the application.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The prompt message for requesting the width and height of the simulation field.
        /// </summary>
        public static readonly string FieldSizePrompt = "Please enter the width and height of the simulation field, separated by a space (e.g., '10 20'):";

        /// <summary>
        /// The prompt message for requesting the starting position and orientation of a car, without a car name.
        /// </summary>
        public static readonly string CarPositionPrompt = "Please enter the starting position (X Y) and orientation (N, E, S, W) of the car: (e.g., '1 2 N')";

        /// <summary>
        /// The prompt message for requesting the starting position and orientation of a car, with a car name.
        /// </summary>
        public static readonly string CarPositionPromptWithName = "Please enter the position (X Y) and facing direction (N, E, S, W) for the car named {0}:";

        /// <summary>
        /// The prompt message for requesting the sequence of commands for a car, without a car name.
        /// </summary>
        public static readonly string CommandsPrompt = "Please enter the sequence of commands for the car: (e.g., 'LFFR') where 'L' = turn left, 'R' = turn right, 'F' = move forward";

        /// <summary>
        /// The prompt message for requesting the sequence of commands for a car, with a car name.
        /// </summary>
        public static readonly string CommandsPromptWithName = "Please enter the sequence of commands for {0} car: (e.g., 'LFFR') where 'L' = turn left, 'R' = turn right, 'F' = move forward";

        /// <summary>
        /// The prompt message for entering a car name.
        /// </summary>
        public static readonly string EnterCarNamePrompt = "Enter car name:";

        /// <summary>
        /// The prompt message for adding another car.
        /// </summary>
        public static readonly string AddAnotherCarPrompt = "Add another car? (y/n):";

        /// <summary>
        /// The message indicating no collision occurred during the simulation.
        /// </summary>
        public static readonly string NoCollisionMessage = "No collision";

        /// <summary>
        /// Name of the default car in single car situation.
        /// </summary>
        public static readonly string DefaultCarName = "UnnamedCar";

        /// <summary>
        /// The error message for an invalid field size input format.
        /// </summary>
        public static readonly string InvalidFieldSizeFormat = "Invalid format. Please enter width and height as two integers separated by a space (e.g., '10 20').";

        /// <summary>
        /// The error message for an invalid car details input format.
        /// </summary>
        public static readonly string InvalidCarDetailsFormat = "Invalid format. Please enter position and orientation as 'X Y Orientation' (e.g., '1 2 N').";

        /// <summary>
        /// The error message for an invalid command format.
        /// </summary>
        public static readonly string InvalidCommandFormat = "Invalid command '{0}'. Only 'L', 'R', and 'F' are allowed.";

        /// <summary>
        /// The error message for general input format exceptions.
        /// </summary>
        public static readonly string GeneralInputFormatError = "Error: {0} Please try again.";
        /// <summary>
        /// The prompt message for requesting the simulation type.
        /// </summary>
        public static readonly string SimulationTypePrompt = "\nChoose simulation type:\n1. Single Car Simulation\n2. Multiple Cars Simulation\nType 'q' to exit.\nEnter your choice (1 or 2): ";
        
        /// <summary>
        /// ASCII Art for the welcome display.
        /// </summary>
        public static readonly string WelcomeMessage = @"                                              Welcome to the Car Simulation Program!


                                             __..-======-------..__
                                          . '    ______    ___________`.
                                        .' .--. '.-----.`. `.-----.-----`.
                                       / .'   | ||      `.` \     \     \            _
                                     .' /     | ||        \ \_____\\_____\\__________[_]
                                    /   `-----' |'---------`\  .'                       \
                                   /============|============\'-------------------.._____|
                                .-`---.         |-==.        |'.__________________  =====|-._
                                .`        `.      |            |      .--------.    _` ====|  _ .
                                /     __     \     |            |   .'           `. [_] `.==| [_] \
                                [   .`    `.  |     |            | .'     .---.     \      \=|     |
                                |  | / .-. '  |_____\\___________/_/     .'---. `.    |     | |     |
                                `-'| | O |'..`------------------'.....'/ .-. \ |    |       ___.--'
                                    \ `-' / /   `._.'                 | | O | |'___...----''___.--'
                                     `._.'.'                           \ `-' / [___...----''_.']
                                                                       `._.'.'
   
                                          Prepare for a high-speed simulation challenge!";
    }
}
