namespace Config.IniFiles.Errors
{
    /// <summary>
    ///     Raised when value could not be parsed correctly.
    /// </summary>
    public sealed class NoSectionError : FormatError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NoSectionError" /> class.
        /// </summary>
        /// <param name="line">The line.</param>
        public NoSectionError(int line)
            : base(
                "Config option found but no parent section found before", line)
        {
        }
    }
}