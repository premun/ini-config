namespace Config.IniFiles.Errors
{
    /// <summary>
    ///     Raised when section is found multiple times in one config file.
    /// </summary>
    public sealed class DuplicateSectionError : FormatError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DuplicateSectionError" /> class.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="line">The line.</param>
        public DuplicateSectionError(string sectionName, int line)
            : base("Section '" + sectionName + "' found multiple times.", line)
        {
        }
    }
}