namespace Config.IniFiles.Errors
{
    /// <summary>
    ///     Raised when identifier contains invalid characters.
    /// </summary>
    public sealed class InvalidIdentifierError : FormatError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidIdentifierError" /> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="line">The line.</param>
        public InvalidIdentifierError(string identifier, int line)
            : base("Identifier '" + identifier + "' is invalid.", line)
        {
        }
    }
}