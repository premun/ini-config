using Config.Format.OptionSpecifiers;

namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when value could not be parsed correctly.
	/// </summary>
	public class InvalidValueError : FormatError
	{
		public InvalidValueError(string optionName, string value, OptionSpecifier formatSpecifier, int line)
			: base(
				"Option '" + optionName + "' could not be parsed as " +
				formatSpecifier.GetType().Name.Replace("OptionSpecifier", "").ToLower() + " (value: '" + value + "')", line)
		{
		}
	}
}