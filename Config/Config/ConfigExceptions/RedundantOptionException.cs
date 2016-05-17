namespace Config.ConfigExceptions
{
    /// <summary>
    /// The exception that is thrown when there is a redundant option in the loaded config file.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
    public sealed class RedundantOptionException : ConfigException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedundantOptionException"/> class.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="optionName">Name of the redundant option.</param>
        public RedundantOptionException(string sectionName, string optionName)
            : base("Not specified option '" + sectionName + "#" + optionName + "' found")
        {
        }
    }
}