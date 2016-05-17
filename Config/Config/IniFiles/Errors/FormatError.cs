namespace Config.IniFiles.Errors
{
    /// <summary>
    ///     Signalizes the format errors of the config file.
    /// </summary>
    public abstract class FormatError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatError" /> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="line">The error line.</param>
        protected FormatError(string message, int line)
        {
            Line = line;
            Message = message;
        }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        ///     Gets the line where the error is detected.
        /// </summary>
        /// <value>
        ///     The line.
        /// </value>
        public int Line { get; private set; }
    }
}