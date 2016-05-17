namespace Config.ConfigExceptions
{
    /// <summary>
    ///     The exception that is thrown when there is a missing option in the loaded config file.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
    public sealed class MissingOptionException : ConfigException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingOptionException" /> class.
        /// </summary>
        /// <param name="sectionName">Name of the missing section.</param>
        /// <param name="optionName">Name of the missing option.</param>
        public MissingOptionException(string sectionName, string optionName)
            : base(
                "Required option '" + sectionName + "#" + optionName +
                "' not found")
        {
        }
    }
}