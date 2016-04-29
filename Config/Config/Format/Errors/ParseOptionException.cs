using Config.Format.OptionSpecifiers;

namespace Config.Format.Errors
{
	/// <summary>
	/// Thrown when section is found multiple times in one config file.
	/// </summary>
	public class ParseOptionException : ConfigException
	{
		public ParseOptionException(string optionName, OptionSpecifier specifier)
			: base("Option '" + optionName + "' could not be parsed to '" + specifier.GetType().Name + "'.")
		{
		}
	}
}