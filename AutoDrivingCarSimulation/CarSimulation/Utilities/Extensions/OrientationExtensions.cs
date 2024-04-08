using CarSimulation.Utilities.Constants;

namespace CarSimulation.Utilities.Extensions
{
    /// <summary>
    /// Provides extension methods for the Orientation enum.
    /// </summary>
    public static class OrientationExtensions
    {
        /// <summary>
        /// Converts the Orientation enum value to its shorthand representation.
        /// </summary>
        /// <param name="orientation">The Orientation enum value.</param>
        /// <returns>The shorthand representation of the Orientation.</returns>
        public static string ToShorthandString(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.N:
                    return "North (N)";
                case Orientation.S:
                    return "South (S)";
                case Orientation.E:
                    return "East (E)";
                case Orientation.W:
                    return "West (W)";
                default:
                    return orientation.ToString();
            }
        }
    }
}
