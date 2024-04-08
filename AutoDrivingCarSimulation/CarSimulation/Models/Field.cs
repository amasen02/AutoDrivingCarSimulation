namespace CarSimulation.Models
{
    /// <summary>
    /// Represents the simulation field with boundaries.
    /// </summary>
    public class Field
    {
        public int Width { get; }
        public int Height { get; }

        public Field(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Determines if a given position is within the field boundaries.
        /// </summary>
        public bool IsInsideBounds((int X, int Y) position) =>
            position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
    }
}