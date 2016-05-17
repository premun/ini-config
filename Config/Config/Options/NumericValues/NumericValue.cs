namespace Config.Options.NumericValues
{
    /// <summary>
    ///     Holds information about loaded numeric value.
    /// </summary>
    public abstract class NumericValue
    {
        /// <summary>
        ///     Gets the cleaned string value (without type prefix).
        /// </summary>
        /// <value>
        ///     The string value.
        /// </value>
        public abstract string StringValue { get; }

        /// <summary>
        ///     Gets the number of numeric system (2, 8, 10, 16).
        /// </summary>
        /// <value>
        ///     The numeric system.
        /// </value>
        public abstract int NumericSystem { get; }
    }
}