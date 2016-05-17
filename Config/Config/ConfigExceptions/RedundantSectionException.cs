namespace Config.ConfigExceptions
{
    /// <summary>
    ///     The exception that is thrown when there is a redundant section in the loaded config file.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
    public sealed class RedundantSectionException : ConfigException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RedundantSectionException" /> class.
        /// </summary>
        /// <param name="sectionName">Name of the redundant section.</param>
        public RedundantSectionException(string sectionName)
            : base("Not specified section '" + sectionName + "' found")
        {
        }
    }
}