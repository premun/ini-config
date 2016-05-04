using Config.Format.OptionSpecifiers;

namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when value could not be parsed correctly.
	/// </summary>
	public class InvalidValueError : FormatError
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidValueError"/> class.
        /// </summary>
        /// <param name="optionName">Name of the option.</param>
        /// <param name="value">The value.</param>
        /// <param name="formatSpecifier">The format specifier.</param>
        /// <param name="line">The line.</param>
		public InvalidValueError(string optionName, string value, OptionSpecifier formatSpecifier, int line)
			: base(
				"Option '" + optionName + "' could not be parsed as " +
				formatSpecifier.GetType().Name.Replace("OptionSpecifier", "").ToLower() + " (value: '" + value + "')", line)
		{
		}
	}
}