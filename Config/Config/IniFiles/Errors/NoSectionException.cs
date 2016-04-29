namespace Config.IniFiles.Errors
{
	/// <summary>
	/// Raised when value could not be parsed correctly.
	/// </summary>
	public class NoSectionException : FormatError
	{
		public NoSectionException(int line)
			: base("Config option found but no parent section found before", line)
		{
		}
	}
}