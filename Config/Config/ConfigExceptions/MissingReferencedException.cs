namespace Config.ConfigExceptions
{
    /// <summary>
    ///     The exception that is thrown when there is a missing reference to other option in the loaded config file.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
    public sealed class MissingReferencedException : ConfigException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingReferencedException" /> class.
        /// </summary>
        /// <param name="section">The name of missing reference section.</param>
        /// <param name="option">The name of missing reference option.</param>
        public MissingReferencedException(string section, string option)
            : base(
                "Reference to non existent option '" + section + "@" + option +
                "'.")
        {
        }
    }
}