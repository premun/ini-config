namespace Config.ConfigExceptions
{
    /// <summary>
    ///     The exception that is thrown when there is a missing section in the loaded config file.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
    public sealed class MissingSectionException : ConfigException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingSectionException" /> class.
        /// </summary>
        /// <param name="sectionName">Name of the missing section.</param>
        public MissingSectionException(string sectionName)
            : base("Required section '" + sectionName + "' not found")
        {
        }
    }
}