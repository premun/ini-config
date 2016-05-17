namespace Config.ConfigExceptions
{
    /// <summary>
    ///     The exception that is thrown when there is a cyclic reference in the config.
    /// </summary>
    /// <seealso cref="Config.ConfigExceptions.ConfigException" />
    public sealed class ReferenceCycleException : ConfigException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReferenceCycleException" /> class.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="option">The first option in the cycle.</param>
        public ReferenceCycleException(string section, string option)
            : base(
                "Cycle reference detected when referencing '${" + section + "#" +
                option + "}'.")
        {
        }
    }
}